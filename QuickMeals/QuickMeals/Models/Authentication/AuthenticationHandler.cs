using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuickMeals.Models.Authentication
{
    //static class for managing authentication
    public static class AuthenticationHandler
    {
        //Has all the logged in users
        private static List<UserTimer> LoggedInUsers = new List<UserTimer>();

        //returns an instance of the AuthenticationContext
        private static AuthenticationContext context
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                var configuration = builder.Build();
                var optionsBuilder = new DbContextOptionsBuilder<AuthenticationContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("Users"));
                return new AuthenticationContext(optionsBuilder.Options);
            }
        }

        //Used to measure the time in between checks to the user's timed out status
        private static DateTime StopWatch;

        //determines how long the user must be inactive to be automatically logged out
        private static readonly int TimeOutMinutes = 15;

        //increments the user's timer by how long it has gone since being refreshed
        private static async void IncrementUserTimeOuts()
        {
            if (StopWatch == null)
                StopWatch = DateTime.Now;
            else
            {
                for (int i = 0; i < LoggedInUsers.Count; i++)
                {
                    DateTime now = DateTime.Now;
                    await new Task(() => 
                        { LoggedInUsers[i].timer += (now - StopWatch).Minutes * 60 + (now - StopWatch).Seconds; } );
                }
                StopWatch = DateTime.Now;
            }
        }

        //removes all users that have been timed out
        private static async void ClearTimedOutUsers()
        {
            for (int i = 0; i < LoggedInUsers.Count; i++)
            {
                if ((LoggedInUsers[i].timer) >= TimeOutMinutes * 60)
                {
                    await new Task(() => LoggedInUsers.Remove(LoggedInUsers[i]));
                }
            }
        }
        private static void RefreshTimer(User user)
        {
            if (user != null)
            {
                UserTimer u = LoggedInUsers.FirstOrDefault(ut => ut.user.Username == user.Username);
                if (u != null)
                    u.timer = 0;
            }
        }

        public static bool UserExists(User user)
        {
            bool userExists = false;
            if (user != null)
                using (AuthenticationContext ctx = context)
                {
                    userExists = ctx.Users.Find(user.Username) != null;
                }
            return userExists;
        }
        public static void CreateUser(User user)
        {
            if (UserExists(user))
                return;
            if (user != null)
            {
                using (AuthenticationContext ctx = context)
                {
                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                }
            }
        }
        public static bool UserSignedIn(User user)
        {
            IncrementUserTimeOuts();
            ClearTimedOutUsers();
            if (user != null)
                foreach (UserTimer u in LoggedInUsers)
                {
                    if (u.user.Username == user.Username) return true;
                }
            return false;
        }
        public static User GetDatabaseInstance(User user)
        {
            using AuthenticationContext ctx = context;
            User dbInstance = ctx.Users.Include(u => u.Role).Where(u => u.Username == user.Username).SingleOrDefault();
            return dbInstance;
        }
        public static bool PassedSignin(User user)
        {
            if (user != null)
            {
                User dbInstance = GetDatabaseInstance(user);
                if (user.Username == dbInstance.Username && user.Password.ToString() == dbInstance.Password) return true;

            }
            return false;
        }
        public static void SignIn(ISession session, User user)
        {
            IncrementUserTimeOuts();
            ClearTimedOutUsers();
            if (user != null)
            {
                session.SetObject<User>("USER", GetDatabaseInstance(user));
                LoggedInUsers.Add(new UserTimer { user = GetDatabaseInstance(user), timer = 0, id = session.Id });
            }
        }
        //removes user from their session and removes their session from the list
        public static void SignOut(ISession session)
        {
            IncrementUserTimeOuts();
            ClearTimedOutUsers();
            LoggedInUsers.Remove(LoggedInUsers.First(ut => ut.user.Username == session.GetObject<User>("USER").Username));
            session.Remove("USER");
        }
        //gets the active user for the given session
        public static User CurrentUser(ISession session)
        {
            IncrementUserTimeOuts();
            ClearTimedOutUsers();
            User user = session.GetObject<User>("USER");
            RefreshTimer(user);
            if (UserSignedIn(user))
            {
                if (LoggedInUsers.First(ut => ut.user.Username == user.Username).id != session.Id)
                {
                    session.Remove("USER");
                    user = null;
                }
            }
            else if (user != null)
            {
                session.Remove("USER");
                user = null;
            }
            if (user == null)
                user = context.Users.Find("");
            return user;
        }
        public static void EditUser(User user, string NewPassword)
        {
            if (UserExists(user))
            {
                using AuthenticationContext ctx = context;
                ctx.Users.Update(user);
                ctx.SaveChanges();
            }
        }
        public static void DeleteUser(User user)
        {
            if (UserExists(user))
            {
                using AuthenticationContext ctx = context;
                ctx.Users.Remove(GetDatabaseInstance(user));
                ctx.SaveChanges();
            }
        }

        protected class UserTimer
        {
            public User user;
            //how long the user has been inactive for
            public int timer;
            //session id of the current user
            public string id;
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuickMeals.Models.Authentication
{
    public static class AuthenticationHandler
    {
        /*
         * sessions are added to this list when signed in and removed from wjen signed out.
         * this provides a way of keeping track of which users are signed in, and when they
         * close out of the browser without signing out, as their session is set to null
         */
        private static List<ISession> Sessions = new List<ISession>();
        //sets the authentication context (note, haven't configured the database that uses it yet)
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
        //remove all sessions that have been closed
        private static void RemoveNullSessions()
        {
            foreach (ISession session in Sessions)
            {
                if (session == null)
                {
                    Sessions.Remove(session);
                }
            }
        }
        //checks whether a user exists in the database
        public static bool UserExists(User user)
        {
            using (AuthenticationContext ctx = context)
            {
                if (ctx.Users.Find(user.Username) != null) return true;
                return false;
            }
        }
        public static bool UserExists(string userName)
        {
            using (AuthenticationContext ctx = context)
            {
                if (ctx.Users.Find(userName) != null) return true;
                return false;
            }
        }
        //Add user to database
        public static void CreateUser(User user)
        {
            using (AuthenticationContext ctx = context)
            {
                ctx.Users.Add(user);
                ctx.SaveChanges();
            }
        }
        //checks whether a user is currently signed into the app
        public static bool UserSignedIn(User user)
        {
            RemoveNullSessions();
            foreach (ISession session in Sessions)
            {
                if (session.Keys.Contains("USER"))
                {
                    User loggedUser = session.GetObject<User>("USER");
                    if (loggedUser != null)
                    {
                        if (loggedUser.Username == user.Username) return true;
                    }
                }
            }
            return false;
        }
        public static bool UserSignedIn(string userName)
        {
            RemoveNullSessions();
            foreach (ISession session in Sessions)
            {
                if (session.Keys.Contains("USER"))
                {
                    User loggedUser = session.GetObject<User>("USER");
                    if (loggedUser != null)
                    {
                        if (loggedUser.Username == userName) return true;
                    }
                }
            }
            return false;
        }
        //gets user from database with all associated information
        public static User GetDatabaseInstance(User user)
        {
            using (AuthenticationContext ctx = context)
                return ctx.Users.Where(u => u.Username == user.Username).Include(u => u.Role).ToArray()[0];
        }
        //checks wether the user has the correct password
        public static bool PassedSignin(User user)
        {
            User dbInstance = GetDatabaseInstance(user);
            if (user.Username == dbInstance.Username && user.Password == dbInstance.Password) return true;
            return false;
        }
        //signs the user into their session, and adds their session to the list
        public static void SignIn(ISession session, User user)
        {
            user = GetDatabaseInstance(user);
            session.SetObject<User>("USER", user);
            Sessions.Add(session);
        }
        //removes user from their session and removes their session from the list
        public static void SignOut(ISession session)
        {
            session.Remove("USER");
            Sessions.Remove(session);
        }
        //gets the active user for the given session
        public static User CurrentUser(ISession session)
        {
            User user = session.GetObject<User>("USER");
            if(user == null)
            {
                user = context.Users.Include(u => u.Role).Where(u => u.RoleID == 0).SingleOrDefault();
            }
            return user;
        }
    }
}
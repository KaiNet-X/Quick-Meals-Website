using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickMeals.Models.Authentication
{
    public static class AuthorizationHandler
    {
        public static bool IsAuthorized(User user, ValidRole role)
        {
            return AuthenticationHandler.GetDatabaseInstance(user).RoleID == (int)role;
        }
        public static bool IsAuthorized(User user, string role)
        {
            return AuthenticationHandler.GetDatabaseInstance(user).Role.RoleName == role;
        }
        public static bool IsSignedIn(ISession session)
        {
            return AuthenticationHandler.CurrentUser(session).RoleID != 0;
        }
        public enum ValidRole
        {
            Anonymous,
            User,
            Admin
        }
    }

}

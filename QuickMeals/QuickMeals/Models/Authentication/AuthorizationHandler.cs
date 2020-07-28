using Microsoft.AspNetCore.Http;

namespace QuickMeals.Models.Authentication
{
    //provides several checks to authorization of a user based on the authentication handler
    public static class AuthorizationHandler
    {
        //checks if current user meets authorization requirements
        public static bool IsAuthorized(User user, ValidRole role)
        {
            return AuthenticationHandler.GetDatabaseInstance(user).RoleID == (int)role;
        }

        //checks if current user meets authorization requirements
        public static bool IsAuthorized(User user, string role)
        {
            return AuthenticationHandler.GetDatabaseInstance(user).Role.RoleName == role;
        }

        //checks if a user is signed in
        public static bool IsSignedIn(ISession session)
        {
            return AuthenticationHandler.CurrentUser(session).RoleID != 0;
        }

        //enum with all set roles
        public enum ValidRole
        {
            Anonymous,
            User,
            Admin
        }
    }

}

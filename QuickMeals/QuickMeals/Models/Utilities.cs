using QuickMeals.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickMeals.Models
{
    public static class Utilities
    {
        public static void UserToView(Controller controller)
        {
            User user = AuthenticationHandler.CurrentUser(controller.HttpContext.Session);

            controller.ViewBag.User = user;
            controller.ViewBag.SignedIn = user.RoleID != 0;
        }

        //decreases amount of calls to Authentication and Authorization handlers
        public static void UserToView(Controller controller, ref bool SignedIn)
        {
            User user = AuthenticationHandler.CurrentUser(controller.HttpContext.Session);
            controller.ViewBag.User = user;

            SignedIn = user.RoleID != 0;
            controller.ViewBag.SignedIn = SignedIn;
        }

        public static void UserToView(Controller controller, ref bool SignedIn, ref User CurrentUser)
        {
            CurrentUser = AuthenticationHandler.CurrentUser(controller.HttpContext.Session);
            controller.ViewBag.User = CurrentUser;

            SignedIn = CurrentUser.RoleID != 0;
            controller.ViewBag.SignedIn = SignedIn;
        }
    }
}

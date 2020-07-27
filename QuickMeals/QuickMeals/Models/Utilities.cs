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
            controller.ViewBag.SignedIn = AuthorizationHandler.IsSignedIn(controller.HttpContext.Session);
            controller.ViewBag.User = AuthenticationHandler.CurrentUser(controller.HttpContext.Session);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickMeals.Data;
using QuickMeals.Models;
using QuickMeals.Models.Authentication;

namespace QuickMeals.Controllers
{
    public class HomeController : Controller
    {
        private QuickMealsContext context { get; set; }
        public HomeController(AuthenticationContext context, QuickMealsContext ctx)
        {
            AuthenticationHandler.context = context;
            this.context = ctx;
        }

        public IActionResult Index()
        {
            ViewBag.SignedIn = AuthorizationHandler.IsSignedIn(HttpContext.Session);
            var Recipes = context.Recipes.OrderBy(mbox => mbox.Title).ToList();
            return View(Recipes);
            //need to put name of view here
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

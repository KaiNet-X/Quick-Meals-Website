using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuickMeals.Data;
using QuickMeals.Models;

namespace QuickMeals.Controllers
{
    public class HomeController : Controller
    {
        private QuickMealsContext context { get; set; }
        public HomeController(QuickMealsContext ctx)
        {
            this.context = ctx;
        }
        //shows list of al recipes
        public IActionResult Index()
        {
            Utilities.UserToView(this);
            var Recipes = context.Recipes.OrderBy(mbox => mbox.Title).ToList();
            return View(Recipes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

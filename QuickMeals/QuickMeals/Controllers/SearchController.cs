using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuickMeals.Data;
using QuickMeals.Models;

namespace QuickMeals.Controllers
{
    public class SearchController : Controller
    {
        private QuickMealsContext context { get; set; }
        public SearchController(QuickMealsContext ctx)
        {
            context = ctx;
        }

        //searches for recipe by keywords it contains
        public ActionResult Results(string searchName)
        {
            Utilities.UserToView(this);
            var recipes = new List<Recipe>();
            // Filter down if necessary
            if (!String.IsNullOrEmpty(searchName))
            {
                recipes = context.Recipes.Where(p => p.Title.ToLower().Contains(searchName.ToLower()) || p.Description.ToLower().Contains(searchName.ToLower())).ToList();
            }
            // Pass your list out to your view
            return View(recipes);
        }
    }
}

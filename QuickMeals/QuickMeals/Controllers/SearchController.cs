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

        
        //passing instance of AdvancedSearch model to Advanced view.
        public ActionResult Advanced()
        {
            AdvancedSearch ads = new AdvancedSearch();
            return View(ads);
        }

        //passing instance of AdvancedSearch model to Advanced view.
        [HttpPost]
        public ActionResult Advanced(AdvancedSearch model)
        {
            var recipes = context.Recipes.ToList();
            //Filter by protein
            if (model.Chicken)
            {
                recipes = recipes.Where(p => p.Title.ToLower().Contains("chicken") || p.Description.ToLower().Contains("chicken")).ToList();
            }
            if(model.Beef)
            {
                recipes = recipes.Where(p => p.Title.ToLower().Contains("beef") || p.Description.ToLower().Contains("beef")).ToList();
            }
            if(model.Tofu)
            {
                recipes = recipes.Where(p => p.Title.ToLower().Contains("tofu") || p.Description.ToLower().Contains("tofu")).ToList();
            }
            if(model.Parsley)
            {
                recipes = recipes.Where(p => p.Title.ToLower().Contains("parsley") || p.Description.ToLower().Contains("parsley")).ToList(); 
            }
            if(model.Rosemary)
            {
                recipes = recipes.Where(p => p.Title.ToLower().Contains("rosemary") || p.Description.ToLower().Contains("rosemary")).ToList();
            }
            if(model.Thyme)
            {
                recipes = recipes.Where(p => p.Title.ToLower().Contains("thyme") || p.Description.ToLower().Contains("thyme")).ToList();
            }
            if (model.Stardust)
            {
                recipes = recipes.Where(p => p.Title.ToLower().Contains("stardust") || p.Description.ToLower().Contains("stardust")).ToList();
            }
            return View("Results", recipes);
        }

    }
}

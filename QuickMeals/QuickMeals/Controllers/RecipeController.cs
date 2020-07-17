using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuickMeals.Data;
using QuickMeals.Models;
using QuickMeals.Models.Authentication;

namespace QuickMeals.Controllers
{
    public class RecipeController : Controller
    {
        // I added this controller to get things rolling. Its not set in stone. feel free to 
        // make changes. I also added Views and Models - LX
        private QuickMealsContext context { get; set; }

        public RecipeController(QuickMealsContext ctx, AuthenticationContext auth)
        {
            context = ctx;
            AuthenticationHandler.context = auth;
        }
        [HttpGet]
        public IActionResult Add()
        {
            if (!AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            ViewBag.SignedIn = AuthorizationHandler.IsSignedIn(HttpContext.Session);
            ViewBag.Action = "Add";
            return View("Edit", new Recipe());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            ViewBag.SignedIn = AuthorizationHandler.IsSignedIn(HttpContext.Session);
            ViewBag.Action = "Edit";
            var recipe = context.Recipes.Find(id);
            return View(recipe);
            //need to add recipe view?
        }
        [HttpPost]
        public IActionResult Edit(Recipe recipe)
        {
            ViewBag.SignedIn = AuthorizationHandler.IsSignedIn(HttpContext.Session);
            if (ModelState.IsValid)
            {
                if (!AuthorizationHandler.IsSignedIn(HttpContext.Session))
                    return RedirectToAction("Index", "Home");
                if (recipe.RecipeId == 0)
                    context.Recipes.Add(recipe);
                else
                    context.Recipes.Update(recipe);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else { ViewBag.Action = (recipe.RecipeId == 0) ? "Add" : "Edit";
                return View(recipe);

            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            ViewBag.SignedIn = AuthorizationHandler.IsSignedIn(HttpContext.Session);
            var recipe = context.Recipes.Find(id);
            return View(recipe);
        }
        [HttpPost]
        public IActionResult Delete(Recipe recipe)
        {
            if (!AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            ViewBag.SignedIn = AuthorizationHandler.IsSignedIn(HttpContext.Session);
            context.Recipes.Remove(recipe);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            ViewBag.SignedIn = AuthorizationHandler.IsSignedIn(HttpContext.Session);
            return View(context.Recipes.ToList());
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private QuickMealsContext context;

        public RecipeController(QuickMealsContext ctx)
        {
            context = ctx;
        }

        //returns add view with username set if user is signed in
        [HttpGet]
        public IActionResult Add()
        {
            if (!AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            Utilities.UserToView(this);
            ViewBag.Action = "Add";
            return View("Edit", new Recipe() { Username = AuthenticationHandler.CurrentUser(HttpContext.Session).Username });
        }

        //returns edit view if user is signed in
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            var recipe = context.Recipes.Find(id);

            if (AuthenticationHandler.CurrentUser(HttpContext.Session).Username != recipe.Username)
                return RedirectToAction("Index", "Home");

            Utilities.UserToView(this);
            ViewBag.Action = "Edit";
            return View(recipe);
        }

        //adds or edits the recipe returned to it
        [HttpPost]
        public IActionResult Edit(Recipe recipe)
        {
            Utilities.UserToView(this);
            if (ModelState.IsValid)
            {
                if (!AuthorizationHandler.IsSignedIn(HttpContext.Session))
                    return RedirectToAction("Index", "Home");
                if (AuthenticationHandler.CurrentUser(HttpContext.Session).Username != recipe.Username)
                    return RedirectToAction("Index", "Home");
                int ID = recipe.RecipeId;
                if (recipe.RecipeId == 0)
                {
                    context.Recipes.Add(recipe);
                    context.SaveChanges();
                    ID = context.Recipes.OrderByDescending(r => r.RecipeId).FirstOrDefault().RecipeId;
                }
                else
                {
                    context.Recipes.Update(recipe);
                    context.SaveChanges();
                }
                //check if file has been uploaded
                if (recipe.File != null)
                {
                    string FilePath = Directory.GetCurrentDirectory() + "/wwwroot/RecipeImages";
                    string name = $"/{ID}{Path.GetExtension(recipe.File.FileName)}";
                    //create and dispose of file stream from memory. Must be set to a file name and not a folder
                    using (Stream str = new FileStream(FilePath + name, FileMode.Create))
                    {
                        //copy file from ram into storage
                        recipe.File.CopyTo(str);
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            else { ViewBag.Action = (recipe.RecipeId == 0) ? "Add" : "Edit";
                return View(recipe);

            }
        }

        //returns view to delete the recipe by id if signed in
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            Utilities.UserToView(this);
            var recipe = context.Recipes.Find(id);
            return View(recipe);
        }

        //deletes recipe along with recipe image if one exists
        [HttpPost]
        public IActionResult Delete(Recipe recipe)
        {
            if (!AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");

            Utilities.UserToView(this);

            if (System.IO.File.Exists(Recipe.Base + recipe.GetFileName()))
                System.IO.File.Delete(Recipe.Base + recipe.GetFileName());

            context.Recipes.Remove(recipe);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        //returns a view with detais of the recipe including the photo if one exists
        public IActionResult Details(Recipe recipe)
        {
            Utilities.UserToView(this);
            return View(recipe);
        }

        //view with all recipes
        public IActionResult Index()
        {
            Utilities.UserToView(this);
            return View(context.Recipes.ToList());
        }

        //returns view with recipe posted by the signed in user
        public IActionResult MyRecipes()
        {
            bool SignedIn = true;
            User user = new User();
            Utilities.UserToView(this, ref SignedIn, ref user);

            if (!SignedIn)
                return RedirectToAction("Index", "Home");

            user = AuthenticationHandler.GetDatabaseInstance(user);
            List<Recipe> recipes = context.Recipes.Where(r => r.Username == user.Username).ToList();

            return View(recipes);
        }
    }
}

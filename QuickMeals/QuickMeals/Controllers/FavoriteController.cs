using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using QuickMeals.Models;
using QuickMeals.Data;
using Newtonsoft.Json;


namespace QuickMeals.Controllers
{
    public class FavoriteController : Controller
    {
        //Trying to get Favorites working - LX

        private QuickMealsContext context;

        public FavoriteController(QuickMealsContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public ViewResult Index()
        {
            Utilities.UserToView(this);
            var session = new SessionClass(HttpContext.Session);
            var model = new ListViewModel
            {
                Activeones = session.GetRecipes(),
                    
                Activetwos = session.GetRecipes(),
                   
                Recipes = session.GetRecipes()
            };
            return View(model);
        }
        [HttpPost]
        public RedirectToActionResult Delete()
        {
            Utilities.UserToView(this);
            var session = new SessionClass(HttpContext.Session);
            var cookies = new RecipeCookies(Response.Cookies);

            session.RemoveMyRecipes();
            cookies.RemoveMyRecipeIds();

            TempData["message"] = "Favorite recipes cleared";
            return RedirectToAction("Index", "Home",
                new
                {
                    ActiveRec = session.GetRecipes(),

            });

        }

 
        public RedirectToActionResult Add(int activeones, string activetwos)
        {
            Utilities.UserToView(this);
            //created variable to save recipe
            var selectRecipe = context.Recipes
                //find recipe ID
                .Where(t => t.RecipeId == activeones)
                .FirstOrDefault(); //if recipeId not valid then null
            // a session to save the favorite recipe to
            var session = new SessionClass(HttpContext.Session);
            //get favorite recipe
            var favoriteRecipe = session.GetRecipes();
            //check if favorite recipe is already on the list by comparing recipe id with activeones
            var existingRecipe = favoriteRecipe.Where(x => x.RecipeId == activeones)
                .FirstOrDefault();
            //loop to add recipe if not on list
            if(existingRecipe == null)
            {
                favoriteRecipe.Add(selectRecipe);
                session.SetMyRecipes(favoriteRecipe);
            }
            
       
            TempData["message"] = $"{activetwos} added to your favorites";
            
            return RedirectToAction("Index");
            
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using QuickMeals.Models;
using QuickMeals.Data;
using Newtonsoft.Json;
//using QuickMeals.Models.SessionHandlers;

namespace QuickMeals.Controllers
{
    public class FavoriteController : Controller
    {
        //Trying to get Favorites working - LX
 
            [HttpGet]
            public ViewResult Index()
            {
                var session = new SessionClass(HttpContext.Session);
                var model = new ListViewModel
                {
                    Activeones = session.GetRecipes(),
                    //activeones = session.GetActiveRec(),
                    Activetwos = session.GetRecipes(),
                    //activetwos = session.GetActiveRec(),
                    Recipes = session.GetRecipes()
                };
                return View(model);
            }
            [HttpPost]
            public RedirectToActionResult Delete()
            {
                var session = new SessionClass(HttpContext.Session);
                var cookies = new RecipeCookies(Response.Cookies);

                session.RemoveMyRecipes();
                cookies.RemoveMyRecipeIds();

                TempData["message"] = "Favorite recipes cleared";
                return RedirectToAction("Index", "Home",
                    new
                    {
                        ActiveRec = session.GetRecipes(),
                    //ActiveDiv = session.GetActiveDiv()
                });

            }


            /**
            public IActionResult Index()
            {
                return View();
            }
            **/
    }
}

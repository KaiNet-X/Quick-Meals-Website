using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QuickMeals.Models
{
    //Added cookies to see if its better than sessionclass - lx
    public class RecipeCookies
    {
        private const string MyRecipes = "myrecipes";
        private const string Delimiter = "-";

        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }

        public RecipeCookies(IRequestCookieCollection cookies)
        {
            requestCookies = cookies;
        }
        public RecipeCookies(IResponseCookies cookies)
        {
            responseCookies = cookies;
        }
        public void SetMyRecipeIds(List<Recipe> myrecipes)
        {
            
            // initially it was written as t.RecipeId but it didnt want to put an int into ToList
            // now there is an errror with Select because I added ToString to fix the initial error -lx
            
            List<string> ids = myrecipes.Select(t => t.Title).ToList();
            string idsString = String.Join(Delimiter, ids);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30)
            };
            RemoveMyRecipeIds(); //delete old cookie first
            responseCookies.Append(MyRecipes, idsString, options);
            
        }

        public string[] GetMyRecipeIds()
        {
            string cookie = requestCookies[MyRecipes];
            if (string.IsNullOrEmpty(cookie))
                return new string[] { }; //empty string array
            else
                return cookie.Split(Delimiter);
        }
        public void RemoveMyRecipeIds()
        {
            responseCookies.Delete(MyRecipes);
        }
    }
}

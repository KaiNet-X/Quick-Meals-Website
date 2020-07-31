using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QuickMeals.Models
{
    //created SessionClass for Favorites - lx
    public class SessionClass
    {
        private const string RecipeKey = "myrecipes";
        private const string CountKey = "recipecount";
        //private const string RecKey = "rec";

        private ISession session { get; set; }
        public SessionClass(ISession session)
        {
            this.session = session;
        }
        //sets recipes for current session
        public void SetMyRecipes(List<Recipe> recipes)
        {
            session.SetObject(RecipeKey, recipes);
            session.SetInt32(CountKey, recipes.Count);
        }
        //gets recipes for current session
        public List<Recipe> GetRecipes() =>
            session.GetObject<List<Recipe>>(RecipeKey) ?? new List<Recipe>();
        public int? GetMyTeamCount() => session.GetInt32(CountKey) ?? 0;
        //deletes recipes when user clicks on CLEAR FAVORITES from favorite view
        public void RemoveMyRecipes()
        {
            session.Remove(RecipeKey);
            session.Remove(CountKey);
        }
    }
}

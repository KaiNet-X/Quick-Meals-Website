using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuickMeals.Controllers;

namespace QuickMeals.Models
{
    //I added this model to work with Favorites - lx
    public class ListViewModel
    {
        public List<Recipe> Recipes { get; set; }
        public string Active1 { get; set; }
        public string Active2 { get; set; }

        // make next two properties standard properties so the 
        // setter can make the first item in each list "All"
        private List<Recipe> activeones;
        public List<Recipe> Activeones
        {
            get => activeones;
            set
            {
                activeones = value;
                activeones.Insert(0, new Recipe { RecipeId = 0, Title = "All" });
            }
        }
        private List<Recipe> activetwos;
        public List<Recipe> Activetwos
        {
            get => activetwos;
            set
            {
                activetwos = value;
                activetwos.Insert(0, new Recipe { RecipeId = 0, Title = "All" });
            }
        }
        //methods to help view determine active link
        public string CheckActive1(string c) =>
            c.ToLower() == Active1.ToLower() ? "active" : "";

        public string CheckActive2(string d) =>
            d.ToLower() == Active2.ToLower() ? "active" : "";
    
}
}

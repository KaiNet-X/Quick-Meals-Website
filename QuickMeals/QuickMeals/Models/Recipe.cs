using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuickMeals.Models
{
    public class Recipe
    {
        // Hey guys. I created these columns below to set up the database. I hope this meets what we may need for the webapp
        // Let me know if I need to change anything. -LX
        public int RecipeId { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter a tempting title for your delicious QuickMeal!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Help us prepare your QuickMeal by providing total cook time minutes.")]
        public int CookTime { get; set; }

        [Required(ErrorMessage = "Please enter ingredients for your QuickMeal.")]
        public string Ingredients { get; set; }

        [Required(ErrorMessage ="Please tell us how to make your deliscious QuickMeal!")]
        public string Description { get; set; }
    }
}

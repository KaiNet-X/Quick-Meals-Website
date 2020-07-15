using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuickMeals.Models;

namespace QuickMeals.Data
{
    public class QuickMealsContext : DbContext
    {
        public QuickMealsContext(DbContextOptions<QuickMealsContext> options)
            : base(options)
        { }
        public DbSet<Recipe>Recipes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    RecipeId = 1,
                    UserId = 1,
                    CookTime = 30,
                    Title = "Creamy Herb Chicken",
                    Ingredients = "Chicken, garlic, oil, parsley, thyme, rosemary and milk",
                    Description = "1. Coat chicken breasts with onion and garlic " +
                    "powders and herbs. Season generously with salt and pepper. 2. Heat" +
                    "1 Tablespoon of oil and cook chicken breasts. Transfer to plate. 3. Heat" +
                    " oil and saute garlic, parsley, thyme and rosemary. 4. Stir in milk or cream. " +
                    "5. Bring to a boil. Add cornstarch and stir until thickened. 6. Return chicken to" +
                    " skillet and sprinkle extra herbs if desired."

                },
                new Recipe
                {
                    RecipeId = 2,
                    UserId = 2,
                    Title = "Garlic Honey Chicken",
                    CookTime = 15,
                    Ingredients = "Olive oil, chicken breast, honey, soy sauce, garlic, red pepper flakes, salt and pepper",
                    Description = "1. Heat oil over medium heat. 2. Lightly season the cubed chicken with salt and pepper. 3. Add " +
                    "the chicken to the skillet and brown on one side. 4. Make the glaze. Whisk the honey, soy sauce, garlic, " +
                    "and red pepper flakes in a small bowl. 5. Add the sauce to the pan and toss to coat chicken. Cook until " +
                    "chicken is done. 6. Serve on steamed rice."
                }
                );
        }
    }
}

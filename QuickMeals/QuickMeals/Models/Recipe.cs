using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Microsoft.AspNetCore.Http;
using QuickMeals.Models.Authentication;
using QuickMeals.Models.CustomValidation;
using Sujith_Site.Models.CustomAttributes;

namespace QuickMeals.Models
{
    public class Recipe
    {
        // Hey guys. I created these columns below to set up the database. I hope this meets what we may need for the webapp
        // Let me know if I need to change anything. -LX
        public int RecipeId { get; set; }

        [Required(ErrorMessage = "Please enter a tempting title for your delicious QuickMeal!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Help us prepare your QuickMeal by providing total cook time minutes.")]
           //custom attribute
        [MinVal(1)]
        public int CookTime { get; set; }

        [Required(ErrorMessage = "Please enter ingredients for your QuickMeal.")]
        public string Ingredients { get; set; }

        [Required(ErrorMessage = "Please tell us how to make your delicious QuickMeal!")]
        public string Description { get; set; }

        //the primary key for user is a string value, so foreign key needs to bey type string
        public string Username { get; set; } = "";
        //this associates the user object to the recipe object and allows it to be included
        public User User { get; set; }

        //do not add to database
        [NotMapped]
        [AllowedExtensions(new string[] { ".jpeg", ".jpg", ".png", ".img" }, ErrorMessage = "File must be an image")]
        //custom attribute
        [MaxFileSize(50, MaxFileSizeAttribute.FileSizeUnit.MB, ErrorMessage = "Image too large")]
        //used in file uploading to contain the uploaded file
        public IFormFile File { get; set; }

        //static files such as images need to be stored in wwwroot. in order to get a unique name, they are
        //named after the recipe id
        public static string Base => Directory.GetCurrentDirectory() + "/wwwroot/RecipeImages/";

        //public object id { get; internal set; }

        public string GetFileName()
        {
            string[] files = Directory.GetFiles(Base);
            foreach (string s in files)
            {
                if (Path.GetFileNameWithoutExtension(s) == RecipeId.ToString())
                    return Path.GetFileName(s);
            }
            return null;
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace QuickMeals.Models.Authentication
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 15 charachters")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Passowrd must be between 5 and 15 charachters")]
        public string Password { get; set; }

        public string RoleID { get; set; }
        public Role Role { get; set; }
    }
}

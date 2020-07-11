using System.ComponentModel.DataAnnotations;

namespace QuickMeals.Models.Authentication
{
    public class Role
    {
        [Required]
        public int? RoleID { get; set; }
        public string RoleName { get; set; }
    }
}

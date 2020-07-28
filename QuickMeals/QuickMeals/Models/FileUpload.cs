using Microsoft.AspNetCore.Http;

namespace QuickMeals.Models
{
    public class FileUpload
    {
        public IFormFile File { get; set; }
    }
}

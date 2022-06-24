using Movies.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.Controllers
{
    public class MovieDto
    {
        [MaxLength(200)]
        public string Title { get; set; }
        public int ProductionYear { get; set; }

        public float Rate { get; set; }
        [MaxLength(500)]
        public IFormFile? Image { get; set; }
        public string Description { get; set; }
        public List<int> CategoriesId { get; set; }
    }
}
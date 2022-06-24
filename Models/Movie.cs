using System.ComponentModel.DataAnnotations;

namespace Movies.Models
{
    public class Movie
    {
        public Movie()
        {
            Categories=new HashSet<Category>();
        }
        public int Id { get; set; }
        [MaxLength(150)]
        public string Title { get; set; }
        public int ProductionYear { get; set; }

        public float Rate { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}

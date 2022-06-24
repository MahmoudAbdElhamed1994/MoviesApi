using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Movies.Models
{
    public class Category
    {
        public Category()
        {
            Movies = new HashSet<Movie>();
        }
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [JsonIgnore]
        public IEnumerable<Movie> Movies { get; set; }
    }
}

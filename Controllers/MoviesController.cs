using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;
using System.IO;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly string[] _AllowedExtensions = new string[] { ".jpg", ".png" };
        private readonly int _AllowedMaximumSize = 1048576;
        private readonly ApplicationDbContext dbContext;
        public MoviesController(ApplicationDbContext _dbContext)
        {
            dbContext= _dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await dbContext.Movies.Include(m=>m.Categories).ToListAsync();
            return Ok(movies);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromForm]MovieDto movieDto)
        {
            if (_AllowedExtensions.Contains(Path.GetExtension(movieDto.Image.FileName)))
                return BadRequest("Not Acceptable Extension Only Accept Jpg and Png");
            if (movieDto.Image.Length > _AllowedMaximumSize)
                return BadRequest("Exceeds Maximum Size");
            using var stream = new MemoryStream();
            await movieDto.Image.CopyToAsync(stream);
            var movie = new Movie
            {
                Description = movieDto.Description,
                ProductionYear = movieDto.ProductionYear,
                Rate = movieDto.Rate,
                Title = movieDto.Title,
                Image = stream.ToArray(),
                
            };
            await dbContext.AddAsync(movie);
            foreach(int id in movieDto.CategoriesId)
            {
                var category =await dbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);
                movie.Categories.Add(category);
            }
            await dbContext.SaveChangesAsync();
            return Ok(movie);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie=await dbContext.Movies.Include(s=>s.Categories).SingleOrDefaultAsync(c => c.Id == id);
            if(movie==null)
                return NotFound($"Can't Find Movie With Id {id}");
            return Ok(movie);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, MovieDto movieDto)
        {
            var movie = await dbContext.Movies.SingleOrDefaultAsync(m => m.Id == id);
            if (movie == null)
                return NotFound($"Can't Find Movie With Id {id}");
            if (movie.Image == null)
                return BadRequest("Movie Poster Is Required");
            if (_AllowedExtensions.Contains(Path.GetExtension(movieDto.Image.FileName)))
                return BadRequest("Not Acceptable Extension Only Accept Jpg and Png");
            if (movieDto.Image.Length > _AllowedMaximumSize)
                return BadRequest("Exceeds Maximum Size");
            using var stream = new MemoryStream();
            await movieDto.Image.CopyToAsync(stream);
            movie.Image=stream.ToArray();
            movie.Title = movieDto.Title;
            movie.Rate = movieDto.Rate;
            movie.Description=movieDto.Description;
            HashSet<Category> categories = new HashSet<Category>();
            foreach (int categoryId in movieDto.CategoriesId)
            {
                var category = await dbContext.Categories.FindAsync(categoryId);
                categories.Add(category);
            }
            movie.Categories = categories;
            return Ok(movie);
            
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie=await dbContext.Movies.FindAsync(id);
            if (movie == null)
                return NotFound($"Can't Find Movie With Id {id}");
            dbContext.Remove(movie);
            await dbContext.SaveChangesAsync();
            return Ok("Deleted");

        }
        [HttpGet("GenereId")]
        public async Task<IActionResult> GetMoviesByGenerId(int id)
        {
            var category=await dbContext.Categories.FindAsync(id);
            if (category == null)
                return NotFound($"Cant't Find Category With Id {id}");
            var movies = await dbContext.Movies.Where(m => m.Categories.Any(c=>c.Id==id)).ToListAsync();
            return Ok(movies);
        }
    }
}

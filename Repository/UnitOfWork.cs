using Movies.Data;
using Movies.Models;

namespace Movies.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext context;

        public IbaseRepository<Category> Categories { get; private set; }

        public IbaseRepository<Movie> Movies { get; private set; }

        public UnitOfWork(ApplicationDbContext _context)
        {
            context = _context;
            Categories = new BaseRepository<Category>(context);
            Movies = new BaseRepository<Movie>(context);

        }
        public async Task<int> complete()
        {
           return await context.SaveChangesAsync();
        }
    }
}

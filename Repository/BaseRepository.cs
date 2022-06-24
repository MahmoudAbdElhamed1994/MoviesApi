using Microsoft.EntityFrameworkCore;
using Movies.Data;

namespace Movies.Repository
{
    public class BaseRepository<T> : IbaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        public BaseRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task<T> Add(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }
        public async Task<ICollection<T>> GetAll()
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public T Update(T entity)
        {
            context.Set<T>().Update(entity);
            return entity;
        }
    }
}

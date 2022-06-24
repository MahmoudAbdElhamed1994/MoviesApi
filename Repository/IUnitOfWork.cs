using Movies.Models;

namespace Movies.Repository
{
    public interface IUnitOfWork
    {
        IbaseRepository<Category> Categories { get;}
        IbaseRepository<Movie> Movies { get;}

        public Task<int> complete();
    }
}

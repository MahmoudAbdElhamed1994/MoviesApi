namespace Movies.Repository
{
    public interface IbaseRepository<T>where T : class
    {
        public Task<ICollection<T>> GetAll();
        public Task<T> GetById(int id);
        public Task<T> Add(T entity);
        public T Update(T entity);
        public void Delete(T entity);
    }
}

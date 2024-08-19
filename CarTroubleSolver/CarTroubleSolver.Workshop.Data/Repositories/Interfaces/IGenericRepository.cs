namespace CarTroubleSolver.Workshop.Data.Repositories.Interfaces
{
    public interface IGenericRepository<T>
    {
        public Task<T> Add(T entity);
        public Task<T> Get(Guid id);
        public void Remove(Guid id);
        public IQueryable<T> GetAll();
    }
}

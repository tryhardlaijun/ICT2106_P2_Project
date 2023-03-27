namespace SmartHomeManager.Domain.Common
{
    /// <summary>
    /// Most repositories should extend this interface for dependency injection.
    /// </summary>
    /// <typeparam name="T">The entity that the repository will handle.</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        public Task<bool> AddAsync(T entity);
        public Task<T?> GetByIdAsync(Guid id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<bool> UpdateAsync(T entity);
        public Task<bool> DeleteAsync(T entity);
        public Task<bool> DeleteByIdAsync(Guid id);
        public Task<bool> SaveAsync();
    }
}

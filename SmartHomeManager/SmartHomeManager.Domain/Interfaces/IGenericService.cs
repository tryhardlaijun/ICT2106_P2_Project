namespace SmartHomeManager.Domain.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<bool> Add(T entity);
        Task<T?> GetByIdAsync(int id); 
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteByIdAsync(int id);
    }
}

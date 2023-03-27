using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
    public interface IScenarioRepository<T> : IGenericRepository<T> where T : class
    {
        // Get Name by async
        public Task<T?> GetByNameAsync(string name);
    }
}


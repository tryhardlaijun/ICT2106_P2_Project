using SmartHomeManager.Domain.DirectorDomain.Entities;

namespace SmartHomeManager.Domain.DirectorDomain.Interfaces
{
    public interface IHistoryRepository
    {
        public Task<bool> AddAsync(History history);
        public Task<IEnumerable<History>> GetAllAsync();

    }
}

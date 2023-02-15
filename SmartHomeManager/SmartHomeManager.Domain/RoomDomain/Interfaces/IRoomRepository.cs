using System.Linq.Expressions;
using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.Domain.RoomDomain.Interfaces;

public interface IRoomRepository
{
    Task<Room?> Get(Guid roomId);
    Task<IEnumerable<Room>> GetAll();
    IEnumerable<Room> Find(Expression<Func<Room, bool>> predicate);
    void Add(Room entity);
    void AddRange(IEnumerable<Room> entities);
    void Remove(Room entity);
    void RemoveRange(IEnumerable<Room> entities);
    void Update(Room entity);
    IEnumerable<Room> GetRoomsRelatedToAccount(Guid accountId);
    Task SaveChangesAsync();
}
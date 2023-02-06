using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Interfaces;

namespace SmartHomeManager.DataSource.RoomDataSource;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _db;
    private readonly DbSet<Room> _dbSet;

    public RoomRepository(ApplicationDbContext db)
    {
        _db = db;
        _dbSet = _db.Set<Room>();
    }

    public async Task<Room?> Get(Guid id)
    {
        var result = await _dbSet.FindAsync(id);

        return result;
    }

    public async Task<IEnumerable<Room>> GetAll()
    {
        IEnumerable<Room> query = await _dbSet.ToListAsync();
        return query;
    }

    public void Add(Room entity)
    {
        _dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<Room> entities)
    {
        _dbSet.AddRange(entities);
    }

    public void Remove(Room entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<Room> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public void Update(Room room)
    {
        _dbSet.Update(room);
    }

    public IEnumerable<Room> Find(Expression<Func<Room, bool>> predicate)
    {
        IQueryable<Room> query = _dbSet;
        var result = query.Where(predicate);
        return result.ToList();
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }

    public IEnumerable<Room> GetRoomsRelatedToAccount(Guid accountId)
    {
        // retrieving all rooms that has an account ID of the user input
        // IEnumerable<Room> result = _db.Rooms.Include(room => room.AccountId == accountId);

        IEnumerable<Room> result = _db.Rooms.Where(room => room.AccountId == accountId);

        return result.ToList();
    }
}
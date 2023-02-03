using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Interfaces;

namespace SmartHomeManager.DataSource.RoomDataSource;
public class RoomRepository : IRoomRepository
{
    // readonly - can only be set in ctor, can't be set after ctor exits
    protected readonly ApplicationDbContext _db;

    // NOT IN USE, for note taking / learning purposes only
    // internal means only accessible within the same assembly
    // internal DbSet<T> _dbSet

    // dbSet of type T
    protected DbSet<Room> _dbSet;

    // ctor Dependency Injection
    public RoomRepository(ApplicationDbContext db)
    {
        _db = db;

        // sets it to the generic class of the repository
        this._dbSet = _db.Set<Room>();
    }

    public Room? Get(int? id)
    {
        if (id == null) { return null; }

        // similar to SingleOrDefault()
        Room? result = _dbSet.Find(id);

        // returns only the 1st row. If more than 1 row, then it will NOT throw exception.
        // Category resultExample2 = _db.Category.FirstOrDefault(obj => obj.Id == id);

        // returns only 1 row. If more than 1 row, then will throw exception.
        // If there is 0 rows, then will return null.
        // Difference between Find() and SingleOrDefault is that
        // Find() is only for PK
        // SingleOrDefault is able to take an expression (like a lambda func)
        // and find objects that satisfies a given condition
        // Category resultExample3 = _db.Category.SingleOrDefault(obj => obj.Id == id);

        // returns only 1 row. If there is 0 rows, then will throw exception.
        // Category resultExample4 = _db.Category.Single(obj => obj.Id == id);

        return result;
    }

    public IEnumerable<Room> GetAll()
    {
        // test
        // return _dbSet.ToList();

        IQueryable<Room> query = _dbSet;
        return query.ToList();
    }

    public IEnumerable<Room> Find(Expression<Func<Room, bool>> predicate)
    {
        IQueryable<Room> query = _dbSet;
        IQueryable<Room> result = query.Where(predicate);
        return result.ToList();
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
}

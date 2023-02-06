using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.RoomDomain.Mocks;

public class DeviceRepositoryMock : IDeviceInformationServiceMock
{
    protected readonly ApplicationDbContext _db;

    // NOT IN USE, for note taking / learning purposes only
    // internal means only accessible within the same assembly
    // internal DbSet<T> _dbSet

    // dbSet of type T
    protected DbSet<Device> _dbSet;

    // ctor Dependency Injection
    public DeviceRepositoryMock(ApplicationDbContext db)
    {
        _db = db;

        // sets it to the generic class of the repository
        _dbSet = _db.Set<Device>();
    }

    public IEnumerable<Device> GetDevicesInRoom(Guid roomId)
    {
        // IEnumerable<Device> result = _db.Devices.Include(device => device.RoomId == roomId);

        var test = _db.Devices.ToList();

        IEnumerable<Device> result = _db.Devices.Where(device => device.RoomId == roomId);

        return result.ToList();
    }
}
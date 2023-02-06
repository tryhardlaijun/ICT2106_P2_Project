using System.Linq.Expressions;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Mocks;

namespace SmartHomeManager.Domain.RoomDomain.Services;

public class RoomReadService
{
    private readonly IDeviceInformationServiceMock _deviceInformationService;
    private readonly IRoomRepository _roomRepository;

    public RoomReadService(IRoomRepository roomRepository, IDeviceInformationServiceMock deviceInformationService)
    {
        _roomRepository = roomRepository;
        _deviceInformationService = deviceInformationService;
    }

    public async Task<Room?> GetRoomById(Guid roomId)
    {
        return await _roomRepository.Get(roomId);
    }

    public async Task<IEnumerable<Room>> GetAllRooms()
    {
        return await _roomRepository.GetAll();
    }

    public IEnumerable<Room> FindRoomByCondition(Expression<Func<Room, bool>> predicate)
    {
        return _roomRepository.Find(predicate).ToList();
    }

    public IEnumerable<Device> GetDevicesInRoom(Guid roomId)
    {
        return _deviceInformationService.GetDevicesInRoom(roomId);
    }

    // IList allows for more direct manipulation, so IEnumerable is used instead
    public IEnumerable<Room> GetRoomsRelatedToAccount(Guid accountId)
    {
        return _roomRepository.GetRoomsRelatedToAccount(accountId);
    }
}
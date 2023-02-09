using System.Linq.Expressions;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities.DTOs;
using SmartHomeManager.Domain.RoomDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Mocks;

namespace SmartHomeManager.Domain.RoomDomain.Services;

public class RoomReadService : IRoomReadService
{
    private readonly IDeviceInformationServiceMock _deviceInformationService;
    private readonly IRoomRepository _roomRepository;

    public RoomReadService(IRoomRepository roomRepository, IDeviceInformationServiceMock deviceInformationService)
    {
        _roomRepository = roomRepository;
        _deviceInformationService = deviceInformationService;
    }

    public IList<Room> GetRoomsByAccountId(Guid accountId)
    {
        return _roomRepository.GetRoomsRelatedToAccount(accountId).ToList();
    }

    public async Task<GetRoomWebRequest?> GetRoomById(Guid roomId)
    {
        var res = await _roomRepository.Get(roomId);
        if (res == null) return null;

        var ret = new GetRoomWebRequest
        {
            RoomId = res.RoomId,
            Name = res.Name,
            AccountId = res.AccountId
        };

        return ret;
    }

    public async Task<IEnumerable<GetRoomWebRequest>> GetAllRooms()
    {
        var result = await _roomRepository.GetAll();
        var resp = result.Select(room => new GetRoomWebRequest
        {
            RoomId = room.RoomId,
            Name = room.Name,
            AccountId = room.AccountId
        }).ToList();

        return resp;
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
    public IEnumerable<GetRoomWebRequest> GetRoomsRelatedToAccount(Guid accountId)
    {
        var result = _roomRepository.GetRoomsRelatedToAccount(accountId);
        var resp = result.Select(room => new GetRoomWebRequest
        {
            RoomId = room.RoomId,
            Name = room.Name,
            AccountId = room.AccountId
        }).ToList();

        return resp;
    }
}
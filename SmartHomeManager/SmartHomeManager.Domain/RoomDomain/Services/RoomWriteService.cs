using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities.DTOs;
using SmartHomeManager.Domain.RoomDomain.Interfaces;

namespace SmartHomeManager.Domain.RoomDomain.Services;

public class RoomWriteService
{
    private readonly IRoomRepository _roomRepository;

    public RoomWriteService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<GetRoomWebRequest> AddRoom(String name, Guid accountId)
    {
        var newRoom = new Room
        {
            Name = name,
            AccountId = accountId
        };
        
        _roomRepository.Add(newRoom);
        await _roomRepository.SaveChangesAsync();
        
        var ret = new GetRoomWebRequest
        {
            RoomId = newRoom.RoomId,
            Name = newRoom.Name,
            AccountId = newRoom.AccountId
        };
        
        return ret;
    }

    public async Task AddRangeOfRooms(IEnumerable<Room> rooms)
    {
        _roomRepository.AddRange(rooms);
        await _roomRepository.SaveChangesAsync();
    }

    public async Task RemoveRoom(Guid roomId)
    {
        var res = await _roomRepository.Get(roomId);
        if (res == null) return;
        _roomRepository.Remove(res);
        await _roomRepository.SaveChangesAsync();
    }

    public async Task RemoveRangeOfRooms(IEnumerable<Room> rooms)
    {
        _roomRepository.RemoveRange(rooms);
        await _roomRepository.SaveChangesAsync();
    }

    public async Task UpdateRoom(Guid roomId, String name)
    {
        var res = await _roomRepository.Get(roomId);
        if (res == null) return;
        res.Name = name;
        _roomRepository.Update(res);
        await _roomRepository.SaveChangesAsync();
    }
}
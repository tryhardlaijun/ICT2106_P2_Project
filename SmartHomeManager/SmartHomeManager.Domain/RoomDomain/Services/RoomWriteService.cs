using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Interfaces;

namespace SmartHomeManager.Domain.RoomDomain.Services;

public class RoomWriteService
{
    private readonly IRoomRepository _roomRepository;

    public RoomWriteService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public void AddRoom(Room room)
    {
        _roomRepository.Add(room);
    }

    public void AddRangeOfRooms(IEnumerable<Room> rooms)
    {
        _roomRepository.AddRange(rooms);
    }

    public void RemoveRoom(Room room)
    {
        _roomRepository.Remove(room);
    }

    public void RemoveRangeOfRooms(IEnumerable<Room> rooms)
    {
        _roomRepository.RemoveRange(rooms);
    }

    public void UpdateRoom(Room room)
    {
        _roomRepository.Update(room);
    }

    public async Task SaveChangesAsync()
    {
        await _roomRepository.SaveChangesAsync();
    }
}
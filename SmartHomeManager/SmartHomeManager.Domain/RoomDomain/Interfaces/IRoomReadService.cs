using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.Domain.RoomDomain.Interfaces;

public interface IRoomReadService
{
    IList<Room> GetRoomsByAccountId(Guid accountId);
}
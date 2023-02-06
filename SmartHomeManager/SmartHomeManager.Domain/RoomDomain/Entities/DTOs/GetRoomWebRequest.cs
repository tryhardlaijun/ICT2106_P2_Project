namespace SmartHomeManager.Domain.RoomDomain.Entities.DTOs;

public class GetRoomWebRequest
{
    public Guid RoomId { get; set; }
    public string Name { get; set; }
    public Guid AccountId { get; set; }
}
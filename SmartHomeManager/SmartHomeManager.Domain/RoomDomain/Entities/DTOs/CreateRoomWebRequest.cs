namespace SmartHomeManager.Domain.RoomDomain.Entities.DTOs;

public class CreateRoomWebRequest
{
    public string Name { get; set; }
    public Guid AccountId { get; set; }
}
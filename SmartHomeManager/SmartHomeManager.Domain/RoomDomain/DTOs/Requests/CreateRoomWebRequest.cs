namespace SmartHomeManager.Domain.RoomDomain.DTOs.Requests;

public class CreateRoomWebRequest
{
    public string Name { get; set; }
    public Guid AccountId { get; set; }
}
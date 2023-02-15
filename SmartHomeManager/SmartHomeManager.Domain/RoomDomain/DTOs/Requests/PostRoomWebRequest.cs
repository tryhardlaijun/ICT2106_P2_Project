namespace SmartHomeManager.Domain.RoomDomain.DTOs.Requests;

public class PostRoomWebRequest
{
    public string Name { get; set; }
    public Guid AccountId { get; set; }
}
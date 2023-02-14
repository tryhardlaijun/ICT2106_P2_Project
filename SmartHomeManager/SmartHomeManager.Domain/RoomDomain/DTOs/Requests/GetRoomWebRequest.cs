namespace SmartHomeManager.Domain.RoomDomain.DTOs.Requests;

public class GetRoomWebRequest
{
    public Guid RoomId { get; set; }
    public string Name { get; set; }
    public Guid AccountId { get; set; }
}
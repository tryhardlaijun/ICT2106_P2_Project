namespace SmartHomeManager.API.Controllers.NotificationAPIs.ViewModels
{
    public class GetNotificationDTO
    {
        public List<GetNotificationObjectDTO> NotificationObjects { get; set; }
        public ResponseObjectDTO ResponseObject { get; set; }
    }


    public class GetNotificationObjectDTO
    {
        public Guid NotificationId { get; set; }
        public Guid AccountId { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime SentTime { get; set; }
    }
}

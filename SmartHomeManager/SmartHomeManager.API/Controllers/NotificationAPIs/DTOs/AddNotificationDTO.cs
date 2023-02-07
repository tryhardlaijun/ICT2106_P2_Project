namespace SmartHomeManager.API.Controllers.NotificationAPIs.ViewModels
{
    public class AddNotificationDTO
    {
        public AddNotificationObjectDTO NotificationObject { get; set; }
    }

    public class AddNotificationObjectDTO
    {
        public string Message { get; set; }
        public Guid AccountId { get; set; }
    }
}

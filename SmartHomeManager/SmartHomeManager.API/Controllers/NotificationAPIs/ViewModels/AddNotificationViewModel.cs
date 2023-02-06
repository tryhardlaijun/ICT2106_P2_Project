namespace SmartHomeManager.API.Controllers.NotificationAPIs.ViewModels
{
    public class AddNotificationViewModel
    {
        public AddNotificationObjectViewModel NotificationObject { get; set; }
        public ResponseObjectViewModel ResponseObject { get; set; }
    }

    public class AddNotificationObjectViewModel
    {
        public string Message { get; set; }
        public Guid AccountId { get; set; }
    }
}

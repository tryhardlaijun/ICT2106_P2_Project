namespace SmartHomeManager.API.Controllers.NotificationAPIs.ViewModels
{
    public class GetNotificationViewModel
    {
        public List<GetNotificationObjectViewModel> NotificationObjects { get; set; }
        public ResponseObjectViewModel ResponseObject { get; set; }
    }


    public class GetNotificationObjectViewModel
    {
        public Guid NotificationId { get; set; }
        public Guid AccountId { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime SentTime { get; set; }
    }
}

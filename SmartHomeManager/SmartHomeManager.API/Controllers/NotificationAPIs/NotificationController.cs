using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Services;

namespace SmartHomeManager.API.Controllers.NotificationAPIs
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : Controller
    {

        private readonly SendNotificationService _sendNotificationService;
        private readonly ReceiveNotificationService _receiveNotificationService;

        // Dependency Injection of repos to services...
        public NotificationController(IGenericRepository<Notification> notificationRepository)
        {
            _sendNotificationService = new(notificationRepository);
            _receiveNotificationService = new(notificationRepository);
        }

        // API routes....

        // GET /api/notification/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllNotifications()
        {
            try
            {
                IEnumerable<Notification> notifications = (await _receiveNotificationService.GetAllNotificationsAsync()).ToList();
                return StatusCode(200, notifications);
            } 
            catch(Exception ex)
            {
                return StatusCode(500, "Internal Server error!");
            }
        }

        // TODO:    GET /api/notification/{accountId}
        // TODO:    POST /api/notification
    }
}

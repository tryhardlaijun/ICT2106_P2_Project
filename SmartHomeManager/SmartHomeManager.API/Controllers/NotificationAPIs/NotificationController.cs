using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Services;

namespace SmartHomeManager.API.Controllers.NotificationAPIs
{
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

        // GET /api/notification/{accountId}

        // POST /api/notification
        // Request body...
            
    }
}

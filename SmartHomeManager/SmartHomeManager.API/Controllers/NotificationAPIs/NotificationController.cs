using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.API.Controllers.NotificationAPIs.ViewModels;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Interfaces;
using SmartHomeManager.Domain.NotificationDomain.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartHomeManager.API.Controllers.NotificationAPIs
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : Controller
    {

        private readonly SendNotificationService _sendNotificationService;
        private readonly ReceiveNotificationService _receiveNotificationService;

        // Dependency Injection of repos to services...
        public NotificationController(INotificationRepository notificationRepository, IGenericRepository<Account> mockAccountRepository)
        {
            _sendNotificationService = new(notificationRepository, mockAccountRepository);
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

                // Map notfications to view model....
                List<GetNotificationViewModel> getNotifications = new List<GetNotificationViewModel>();

                foreach (var notification in notifications)
                {
                    getNotifications.Add(new GetNotificationViewModel
                    {
                        NotificationId = notification.NotificationId,
                        AccountId = notification.AccountId,
                        NotificationMessage = notification.NotificationMessage,
                        SentTime = notification.SentTime,
                    });
                }


                return StatusCode(200, getNotifications);
            } 
            catch(Exception ex)
            {
                return StatusCode(500, "Internal Server error!");
            }
        }

        // TODO:    GET /api/notification/{accountId}
        // TODO:    POST /api/notification
        [HttpPost]
        public async Task<IActionResult> AddNotification([FromBody]AddNotificationViewModel viewModel)
        { 

            Notification? notification = await _sendNotificationService
                .SendNotification(
                viewModel.Message, 
                viewModel.AccountId
                );

            // If notification request did not work
            if (notification == null)
            {
                return StatusCode(500, "Something went wrong!");
            }

            return StatusCode(200, notification);  
        }
    }
}

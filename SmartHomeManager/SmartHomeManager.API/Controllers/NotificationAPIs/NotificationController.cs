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
            _receiveNotificationService = new(notificationRepository, mockAccountRepository);
        }

        // API routes....

        // GET /api/notification/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllNotifications()
        {
            IEnumerable<Notification> notifications;
            NotificationResult notificationResult;
            int statusCode;
            string statusMessage;


            (notificationResult, notifications) = (await _receiveNotificationService.GetAllNotificationsAsync());

            // Get the status code and coressponding message...
            (statusCode, statusMessage) = MapNotificationResult(notificationResult);

            // Account not found or DB Error....
            if (notificationResult == NotificationResult.Error_AccountNotFound ||
                notificationResult == NotificationResult.Error_DBReadFail)
            {
                return StatusCode(statusCode, statusMessage);
            }

            // Map notfications to view model....
            List<GetNotificationObjectViewModel> getNotifications = new List<GetNotificationObjectViewModel>();

            foreach (var notification in notifications)
            {
                getNotifications.Add(new GetNotificationObjectViewModel
                {
                    NotificationId = notification.NotificationId,
                    AccountId = notification.AccountId,
                    NotificationMessage = notification.NotificationMessage,
                    SentTime = notification.SentTime,
                });
            }

            // Create final ViewModel object to send to the client..
            GetNotificationViewModel viewModel = new GetNotificationViewModel {
                NotificationObjects = getNotifications,
                ResponseObject = new ResponseObjectViewModel {
                    StatusCode = statusCode,
                    ServerMessage = statusMessage
                }
            };


            // Success path...
            return StatusCode(statusCode, viewModel);
        }

        // TODO:    GET /api/notification/{accountId}
        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetNotificationById(Guid accountId)
        {
            // Use the service here...
            IEnumerable<Notification> notifications;
            NotificationResult notificationResult;

             (notificationResult, notifications) = await _receiveNotificationService.GetNotificationsAsync(accountId);

            return StatusCode((int) notificationResult, notifications);
        }


        // TODO:    POST /api/notification
        [HttpPost]
        public async Task<IActionResult> AddNotification([FromBody] AddNotificationViewModel viewModel)
        {
            NotificationResult notificationResult;
            (notificationResult, Notification? notification) = await _sendNotificationService
                .SendNotification(
                viewModel.Message,
                viewModel.AccountId
            );

            // If notification request did not work
            if (notification == null)
            {
                return StatusCode((int) notificationResult, "Something went wrong!");
            }

            return StatusCode((int) notificationResult, notification);
        }

        private Tuple<int, string> MapNotificationResult(NotificationResult notificationResult)
        {
            const string Success = "SUCC: ";
            const string ClientError = "CLIENT_ERR: ";
            const string ServerError = "SERVER_ERR: ";

            switch (notificationResult)
            {
                case NotificationResult.Success:
                    return Tuple.Create(200, Success + "Success!");
                case NotificationResult.Error_AccountNotFound:
                    return Tuple.Create(400, ClientError + "AccountId Not Found.");
                case NotificationResult.Error_DBInsertFail:
                    return Tuple.Create(500, ServerError + "DB Insert Fail.");
                case NotificationResult.Error_DBReadFail:
                    return Tuple.Create(500, ServerError + "DB Read Fail.");
                case NotificationResult.Error_Other:
                    return Tuple.Create(500, ServerError + "Internal Server Error.");
                default:
                    return Tuple.Create(500, ServerError + "Internal Server Error.");
            }
        }
    }
}

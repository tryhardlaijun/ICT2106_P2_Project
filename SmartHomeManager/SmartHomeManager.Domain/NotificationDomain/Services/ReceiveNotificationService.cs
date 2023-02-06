using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Interfaces;

namespace SmartHomeManager.Domain.NotificationDomain.Services
{ 
    public class ReceiveNotificationService
    {
        private readonly INotificationRepository _notificationRepository;


        public ReceiveNotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            // TODO: Pass in accountId
            return await _notificationRepository.GetAllAsync();
        }

        // List, ArrayList, Array...
        public async Task<Tuple<NotificationResult, IEnumerable<Notification>>> GetNotificationsAsync(Guid accountId)
        {

            // TODO: Create logic for Get Notifications by AccountId
            // Use GetAllByIdAsync
            // Check if account exists
            // Receive the top 5 most recent notifications by AccountId (filter the top 5 most recent)

            /*if (accountToBeFound == null)
            {
                System.Diagnostics.Debug.WriteLine("Account not found");
                return Tuple.Create(NotificationResult.Error_AccountNotFound, null);
            }*/


            IEnumerable<Notification> allNotification = await _notificationRepository.GetAllByIdAsync(accountId);
            IEnumerable<Notification> latest5Notification = allNotification.OrderBy(noti => noti.SentTime).TakeLast(5);

            return Tuple.Create(NotificationResult.Success, latest5Notification);
        }
    }
}

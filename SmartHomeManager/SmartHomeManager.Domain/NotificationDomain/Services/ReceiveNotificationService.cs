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
        public async Task<IEnumerable<Notification>> GetNotificationsAsync(Guid accountId)
        {
            // TODO: Create logic for Get Notifications by AccountId
            // Use GetAllByIdAsync
            // Check if account exists
            // Receive the top 5 most recent notifications by AccountId (filter the top 5 most recent)
            return await _notificationRepository.GetAllByIdAsync(accountId);
        }
    }
}

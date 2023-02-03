using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;

namespace SmartHomeManager.Domain.NotificationDomain.Services
{ 
    public class ReceiveNotificationService
    {
        private readonly IGenericRepository<Notification> _notificationRepository;

        public ReceiveNotificationService(IGenericRepository<Notification> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<Notification>> GetNotificationsAsync(Guid accountId)
        {   
            // TODO: Pass in accountId as well

            return await _notificationRepository.GetAllAsync();
        }

    }
}

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
    public class SendNotificationService : ISendNotification
    {
        private readonly IGenericRepository<Notification> _notificationRepository;

        public SendNotificationService(IGenericRepository<Notification> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public bool SendNotification(string notificationMessage, Guid accountId)
        {
            throw new NotImplementedException();
        }
    }
}

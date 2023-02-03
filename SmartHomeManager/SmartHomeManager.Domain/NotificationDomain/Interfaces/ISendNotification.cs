using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.NotificationDomain.Interfaces
{
    public interface ISendNotification
    {
        public bool SendNotification(string notificationMessage, Guid accountId);
    }
}

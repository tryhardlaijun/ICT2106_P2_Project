using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.NotificationDomain.Interfaces
{
    public interface ISendNotification
    {
        public bool SendNotification(string notificationMessage, Guid accountId);
        //Notification? Get(Guid? id);
        //IEnumerable<Notification> GetAll();
        //IEnumerable<Notification> Find(Expression<Func<Notification, bool>> predicate);
        //void Add(Notification entity);
        //void AddRange(IEnumerable<Notification> entities);
        //void Remove(Notification entity);
        //void RemoveRange(IEnumerable<Notification> entities);
    }
}

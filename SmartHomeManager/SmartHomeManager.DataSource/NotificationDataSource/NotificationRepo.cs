using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;

namespace SmartHomeManager.DataSource.NotificationDataSource
{
    public class NotificationRepo : IGenericRepository<Notification>
    {
        public Task<bool> AddAsync(Notification entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Notification entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Notification>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Notification?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Notification entity)
        {
            throw new NotImplementedException();
        }
    }
}

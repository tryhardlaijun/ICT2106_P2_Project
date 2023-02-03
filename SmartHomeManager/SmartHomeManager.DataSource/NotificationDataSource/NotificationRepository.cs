using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;

namespace SmartHomeManager.DataSource.NotificationDataSource
{
    public class NotificationRepository : IGenericRepository<Notification>
    {

        // tmp variables for testing sake...
        private readonly List<Notification> _tempData;
        private readonly Account _tempAccount;
        private readonly int _tempDataCount = 5;
        private readonly Guid _tempAccountGuid = Guid.Parse("e19a7e8f-c286-4d17-b567-327a219a4f1e");


        public NotificationRepository()
        {

            // Set up tmp account for testing...
            _tempAccount = new Account
            {
                AccountId = _tempAccountGuid,
                Email = "test@email.com",
                Username = "test",
                Address = "Singapore 123456",
                Timezone = 8,
                Password = "testpassword"
            };

            // init new list of notifications....
            _tempData = new List<Notification>();

            // Seed data cache...
            for (int i = 0; i < _tempDataCount; i++)
            {
                _tempData.Add(new Notification
                {
                    NotificationId = Guid.NewGuid(),
                    AccountId = _tempAccount.AccountId,
                    NotificationMessage = "Sample Message " + i,
                    SentTime = DateTime.Now,
                    Account = _tempAccount
                });
            }
        }

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

        public async Task<IEnumerable<Notification>> GetAllAsync()
        { 
            // TODO: actual implementation of database get all...
            return _tempData;
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartHomeManager.Domain.NotificationDomain.Services
{
    public class SendNotificationService : ISendNotification
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly MockAccountService _mockAccountService;

        public SendNotificationService(INotificationRepository notificationRepository, IGenericRepository<Account> mockAccountRepository)
        {
            _notificationRepository = notificationRepository;
            _mockAccountService = new MockAccountService(mockAccountRepository);
        }

        public async Task<Tuple<NotificationResult, Notification?>> SendNotification(string notificationMessage, Guid accountId)
        {
            System.Diagnostics.Debug.WriteLine("Notification message:" + notificationMessage);
            // ---------------------------------------------------------------
            // USE THIS FIRST SINCE ACCOUNT SERVICE IS NOT AVAILABLE YET....
            // Call Juleus method to get account...
            //Account? account = await _mockAccountService.GetAccount(accountId

            // Add an account first ...
            Account addAccount = new Account
            {
                AccountId = Guid.NewGuid(),
                Email = "test@email.com",
                Username = "test123",
                Address = "Singapore 000000",
                Timezone = 8,
                Password = "test123password"
            };

            await _mockAccountService.AddAccount(addAccount);

            var accounts = await _mockAccountService.GetAllAccounts();

            foreach (Account iterableAccount in accounts)
            {
                System.Diagnostics.Debug.WriteLine(iterableAccount.AccountId);
            }

            var accountToBeFound = await _mockAccountService.GetAccount(addAccount.AccountId);
            // ---------------------------------------------------------------


            // TODO: Use Juleus Account Service to get an account, then use that account to create a notification, pass it as a FK.
            // TODO: Use Juleus Account Service to check if account exists. if does not exist invalid...
            // TODO: check NotificationMessage for any SQL injection...

            //Check if account exist
            if (accountToBeFound == null)
            {
                System.Diagnostics.Debug.WriteLine("Account not found");
                return Tuple.Create(NotificationResult.Error_AccountNotFound, new Notification());
            }

            //Remove symbol to prevent SQL injection
            notificationMessage = Regex.Replace(notificationMessage, "[^0-9A-Za-z _-]", "");

            // Generate notification object..
            Notification notificationToBeAdded = new Notification
            {
                AccountId = addAccount.AccountId,
                NotificationMessage = notificationMessage,
                SentTime = DateTime.Now,
                Account = addAccount
            };

            bool result = await _notificationRepository.AddAsync(notificationToBeAdded);

            // If something went wrong...
            if (!result)
            {
                return Tuple.Create(NotificationResult.Error_DBInsertFail, notificationToBeAdded);
            }

            return Tuple.Create(NotificationResult.Success, notificationToBeAdded);
            

        }
    }
}

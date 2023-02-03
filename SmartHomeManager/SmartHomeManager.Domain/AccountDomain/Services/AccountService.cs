using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;

namespace SmartHomeManager.Domain.AccountDomain.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<bool> CreateAccount(AccountWebRequest accountRequest) 
        {
            
            Account realAccount = new Account();

            // this is supplied by us
            realAccount.AccountId = new Guid();

            // these are all from the user
            realAccount.Address = accountRequest.Address;
            realAccount.Password = accountRequest.Password;
            realAccount.Email = accountRequest.Email;
            realAccount.Timezone = accountRequest.Timezone;
            realAccount.Username = accountRequest.Username;
            
            if (_accountRepository.GetAllAsync() == null)
            {
                return false;
            }
            
            await _accountRepository.AddAsync(realAccount);
            int result = await _accountRepository.SaveAsync();
            if (result > 0)
            {
                return true;
            }

            return false;
        }
    }
}

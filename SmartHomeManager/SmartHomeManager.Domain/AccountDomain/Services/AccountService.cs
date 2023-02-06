using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.DTOs;
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
        public async Task<string> CreateAccount(AccountWebRequest accountRequest) 
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

            /*if (_accountRepository.GetAllAsync() == null)
            {
                return false;
            }*/
            Account? findAccount = await _accountRepository.GetAccountByEmailAsync(accountRequest.Email);


            if (findAccount != null)
            {
                // return acc alr exists
                return "account exists";
            }

            await _accountRepository.AddAsync(realAccount);
            int result = await _accountRepository.SaveAsync();
            if (result > 0)
            {
                return "account created";
            }

            return "account not added";
        }

        public async Task<Account?> GetAccountByAccountId(Guid id)
        {
            Account? account = await _accountRepository.GetByIdAsync(id);

            if (account == null)
            {
                return null;
            }

            return account;
        }

        public async Task<IEnumerable<Account>> GetAccounts()
        {
            IEnumerable<Account> accounts = await _accountRepository.GetAllAsync();

            if (accounts == null)
            {
                return Enumerable.Empty<Account>();
            }

            return accounts;
        }
        
        public async Task<bool> VerifyLogin(LoginWebRequest login)
        {
            Account? account = await _accountRepository.GetAccountByEmailAsync(login.Email);
            if (account != null)
            {
                if (account.Password == login.Password)
                {
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}

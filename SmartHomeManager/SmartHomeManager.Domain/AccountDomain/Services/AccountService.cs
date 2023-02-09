using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
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
		public async Task<string> CreateAccount(AccountWebRequest accountWebRequest) 
		{
			bool isEmailUnique = await _accountRepository.IsEmailUnique(accountWebRequest.Email);

			if (!isEmailUnique)
			{
				return "Email already in use!";
			}

			// Create new Account object and assign the web request variables to it, except for the password
            Account realAccount = new Account();
			realAccount.AccountId = new Guid();
            realAccount.Address = accountWebRequest.Address;
            realAccount.Email = accountWebRequest.Email;
            realAccount.Timezone = accountWebRequest.Timezone;
            realAccount.Username = accountWebRequest.Username;

            //Hash the password of the user using the newly created Guid as the salt
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: accountWebRequest.Password,
				salt: realAccount.AccountId.ToByteArray(),
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 100000,
				numBytesRequested: 256 / 8));

			realAccount.Password = hashedPassword;

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

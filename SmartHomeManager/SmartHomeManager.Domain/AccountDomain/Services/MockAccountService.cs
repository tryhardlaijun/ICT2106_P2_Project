using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.Common;

namespace SmartHomeManager.Domain.AccountDomain.Services
{
    public class MockAccountService
    {

        private readonly IGenericRepository<Account> _mockAccountRepository;

        public MockAccountService(IGenericRepository<Account> mockAccountRepository)
        {
            _mockAccountRepository = mockAccountRepository;
        }

        public async Task<Account?> GetAccount(Guid id)
        {
            return await _mockAccountRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _mockAccountRepository.GetAllAsync();
        }

        public async Task<bool> AddAccount(Account account)
        {
            return await _mockAccountRepository.AddAsync(account);
        }
    }
}

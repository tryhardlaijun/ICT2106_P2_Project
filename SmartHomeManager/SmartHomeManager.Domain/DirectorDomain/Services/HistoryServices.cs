using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DirectorDomain.Services
{
    public class HistoryServices
    {
        private readonly IGenericRepository<History> _historyRepository;
        public HistoryServices(IGenericRepository<History> historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public async Task<IEnumerable<History>> GetAllRulesAsync()
        {
            /*History newHist = new History();
            newHist.HistoryId = Guid.NewGuid();
            newHist.Message = "First History";
            newHist.Timestamp = DateTime.Now;
            newHist.ProfileId = Guid.NewGuid();
            newHist.RuleHistoryId = Guid.NewGuid();

            await _historyRepository.AddAsync(newHist);*/

            return await _historyRepository.GetAllAsync();
        }
    }
}

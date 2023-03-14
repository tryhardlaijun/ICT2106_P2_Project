using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
    public class TroubleshooterServices
    {
        private readonly IGenericRepository<Troubleshooter> _troubleshooterRepository;


        public TroubleshooterServices(IGenericRepository<Troubleshooter> troubleshooterRepository)
        {
            _troubleshooterRepository = troubleshooterRepository;
        }

      

        public async Task<IEnumerable<Troubleshooter>> GetTroubleshootersAsync()
        {
            return await _troubleshooterRepository.GetAllAsync();

        }
       

      
       
       

    }
}

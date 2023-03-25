using SmartHomeManager.Domain.SceneDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
    public class GetTroubleshooterServices : IGetTroubleshooterService
    {
        private readonly IGenericRepository<Troubleshooter> _troubleshooterRepository;

        public GetTroubleshooterServices(IGenericRepository<Troubleshooter> troubleshooterRepository){
            _troubleshooterRepository = troubleshooterRepository;
        }

        public async Task<IEnumerable<Troubleshooter>> GetAllTroubleshooter()
        {
            return await _troubleshooterRepository.GetAllAsync();
        }

        public Task<Troubleshooter> GetTroubleshooterById(Guid id)
        {
            throw new NotImplementedException();
        }

        //public async Task<Troubleshooter> GetTroubleshooterById(Guid id)
        //{
        //    return await _troubleshooterRepository.GetByIdAsync(id);
        //}



    }
}

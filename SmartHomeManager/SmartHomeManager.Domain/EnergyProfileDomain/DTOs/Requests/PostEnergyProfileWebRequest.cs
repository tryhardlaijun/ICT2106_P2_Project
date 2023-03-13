using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.EnergyProfileDomain.DTOs.Requests
{
    public class PostEnergyProfileWebRequest
    {
        public Guid AccountId { get; set; }
        public int ConfigValue { get; set; }
    }
}

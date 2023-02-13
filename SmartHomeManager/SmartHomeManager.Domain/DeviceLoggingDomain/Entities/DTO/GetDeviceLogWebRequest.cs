using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DTO
{
    public class GetDeviceLogWebRequest
    {

        public Guid LogId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }


        public DateTime DateLogged { get; set; }

        public int DeviceEnergyUsage { get; set; }


        public int DeviceActivity { get; set; }


        public bool DeviceState { get; set; }

    }
}

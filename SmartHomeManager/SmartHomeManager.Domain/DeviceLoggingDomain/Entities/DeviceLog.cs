using SmartHomeManager.Domain.DeviceDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Entities
{
    public class DeviceLog
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid LogId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Required]
        public DateTime DateLogged { get; set; }

        [Required]
        public int DeviceEnergyUsage { get; set; }

        [Required]
        public int DeviceActivity { get; set; }

        [Required]
        public bool DeviceState { get; set; }

        [Required]
        public Guid DeviceId { get; set; }

        [ForeignKey("DeviceId")]
        public Device Device { get; set; }
    }
}

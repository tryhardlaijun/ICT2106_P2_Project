using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
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

        public DateTime? EndTime { get; set; }

        [Required]
        public DateTime DateLogged { get; set; }

        [Required]
        public double DeviceEnergyUsage { get; set; }

        [Required]
        public double DeviceActivity { get; set; }

        [Required]
        public bool DeviceState { get; set; }

        [Required]
        public Guid DeviceId { get; set; }

        [Required]
        public Guid RoomId { get; set; }

        [ForeignKey("DeviceId")]
        public Device Device { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }
    }


}

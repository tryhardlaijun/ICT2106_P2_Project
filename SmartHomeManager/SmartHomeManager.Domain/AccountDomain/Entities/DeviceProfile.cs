using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.AccountDomain.Entities
{
    public class DeviceProfile
    {
        [Required]
        public Guid DeviceId { get; set; }

        [Required]
        public Guid ProfileId { get; set; }

        [ForeignKey("DeviceId")]
        public Device Device { get; set; }

        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
    }
}

using SmartHomeManager.Domain.AccountDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.HomeSecurityDomain.Entities
{
    public class HomeSecuritySetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid HomeSecuritySettingId { get; set; }

        [Required] public bool Enabled { get; set; }

        [Required] public string DeviceGroup { get; set; }

        [Required] public Guid HomeSecurityId { get; set; }

        [ForeignKey("DeviceGroup")]
        public HomeSecurityDeviceDefinition HomeSecurityDeviceDefinition { get; set; }

        [ForeignKey("HomeSecurityId")]
        public HomeSecurity HomeSecurity { get; set; }
    }
}

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
    public class HomeSecurityDeviceDefinition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DeviceGroup { get; set; }

        [Required] public string ConfigurationKey { get; set; }

        [Required] public int ConfigurationOffValue { get; set; }

        [Required] public int ConfigurationOnValue { get; set; }
    }
}

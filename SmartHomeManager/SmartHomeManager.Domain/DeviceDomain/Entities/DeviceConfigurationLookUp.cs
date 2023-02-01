using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeManager.Domain.DeviceDomain.Entities
{
    public class DeviceConfigurationLookUp
    {
        [Key]
        public string ConfigurationKey { get; set; }

        [Key]
        public string DeviceBrand { get; set; }

        [Key]
        public string DeviceModel { get; set; }

        [Required]
        public string ConfigurationValue { get; set; }

        [Required]
        public string ValueMeaning { get; set; }
    }
}

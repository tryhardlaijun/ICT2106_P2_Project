using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeManager.Domain.DeviceDomain.Entities
{
    public class DeviceConfiguration
    {
        [Key]
        public string ConfigurationKey { get; set; }

        [Key]
        public string DeviceBrand { get; set; }

        [Key]
        public string DeviceModel { get; set; }

        [Key]
        public Guid DeviceId { get; set; }
        
        [Required]
        public int ConfigurationValue { get; set; }

        [ForeignKey("ConfigurationKey, DeviceBrand, DeviceModel")]
        public DeviceConfigurationLookUp DeviceConfigurationLookUp { get; set; }

        [ForeignKey("DeviceId")]
        public Device Device { get; set; }
    }
}

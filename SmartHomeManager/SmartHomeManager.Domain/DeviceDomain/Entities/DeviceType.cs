using System.ComponentModel.DataAnnotations;

namespace SmartHomeManager.Domain.DeviceDomain.Entities
{
    public class DeviceType
    {
        [Key]
        public string DeviceTypeName { get; set; }
    }
}

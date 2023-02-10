using System.ComponentModel.DataAnnotations;

namespace SmartHomeManager.Domain.DeviceDomain.Entities.DTOs
{
    public class DeviceWebRequest
    {
        [Required]
        public string DeviceName { get; set; }

        [Required]
        public string DeviceBrand { get; set; }

        [Required]
        public string DeviceModel { get; set; }

        [Required]
        public string DeviceTypeName { get; set; }

        [Required]
        public string DeviceSerialNumber { get; set; }

        [Required]
        public Guid AccountId { get; set; }
    }
}

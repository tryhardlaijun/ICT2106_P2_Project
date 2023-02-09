using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeManager.Domain.DeviceDomain.Entities
{
    public class Device
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DeviceId { get; set; }

        [Required]
        public string DeviceName { get; set; }

        [Required]
        public string DeviceBrand { get; set; }

        [Required]
        public string DeviceModel { get; set; }

        [Required]
        public string DeviceTypeName { get; set; }

        public Guid? RoomId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public Guid ProfileId { get; set; }

        [ForeignKey("DeviceTypeName")]
        public DeviceType DeviceType { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        [InverseProperty("Devices")]
        public ICollection<Profile> Profiles { get; set; }

        public DeviceCoordinate DeviceCoordinate { get; set; }
    }
}

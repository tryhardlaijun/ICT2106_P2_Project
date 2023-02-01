using SmartHomeManager.Domain.AccountDomain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeManager.Domain.RoomDomain.Entities
{
    public class Room
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RoomId { get; set; }
        
        [Required]
        public int XCoordinate { get; set; }

        [Required]
        public int YCoordinate { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}

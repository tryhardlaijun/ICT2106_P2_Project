using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;

namespace SmartHomeManager.Domain.RoomDomain.Entities
{
    public class RoomCoordinate
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RoomCoordinateId { get; set; }

        [Required]
        public int XCoordinate { get; set; }

        [Required]
        public int YCoordinate { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public Guid RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

    }
}

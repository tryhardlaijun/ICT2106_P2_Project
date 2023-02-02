﻿using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeManager.Domain.RoomDomain.Entities
{
    public class Room
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RoomId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        public RoomCoordinate RoomCoordinate { get; set; }

        public Device Device { get; set; }
    }
}

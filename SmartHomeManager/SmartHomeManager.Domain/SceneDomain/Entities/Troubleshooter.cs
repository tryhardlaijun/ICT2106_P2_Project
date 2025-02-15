﻿using SmartHomeManager.Domain.DeviceDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.SceneDomain.Entities
{
    public class Troubleshooter
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TroubleshooterId { get; set; }

        
        public string? Recommendation { get; set; }

        //[ForeignKey("DeviceTypeName")]
        //public DeviceType? DeviceType { get; set; }
        public string? DeviceType { get; set; }

        public string? ConfigurationKey { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeManager.Domain.SceneDomain.Entities.DTOs
{
    public class TroubleshooterRequest
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TroubleshooterId { get; set; }

        public string? Recommendation { get; set; }

        public string? DeviceType { get; set; }

        public string? ConfigurationKey { get; set; }
    }
}

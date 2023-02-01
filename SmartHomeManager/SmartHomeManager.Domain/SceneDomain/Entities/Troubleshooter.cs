using SmartHomeManager.Domain.DeviceDomain.Entities;
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

        [Required]
        public string Recommendation { get; set; }

        [Required]
        public Guid DeviceId { get; set; }

        [ForeignKey("DeviceId")]
        public Device Device { get; set; }
    }
}

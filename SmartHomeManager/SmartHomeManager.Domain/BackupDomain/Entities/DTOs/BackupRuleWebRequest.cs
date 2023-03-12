using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.BackupDomain.Entities.DTOs
{
    public class BackupRuleWebRequest
    {
        [Required]
        public Guid profileId { get; set; }
        [Required]
        public Guid backupId { get; set; }
    }
}

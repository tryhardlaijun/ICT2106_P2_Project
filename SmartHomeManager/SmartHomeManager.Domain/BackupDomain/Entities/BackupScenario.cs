using SmartHomeManager.Domain.AccountDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.BackupDomain.Entities
{
    public class BackupScenario
    {
        [Key]
        public Guid BackupId { get; set; }

        [Key]
        public Guid ScenarioId { get; set; }

        [Required]
        public string ScenarioName { get; set; }

        [Required]
        public Guid ProfileId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }        
    }
}

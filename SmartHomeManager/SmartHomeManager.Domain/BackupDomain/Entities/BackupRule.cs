using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.BackupDomain.Entities
{
    public class BackupRule
    {
        [Key]
        public Guid BackupId { get; set; }

        [Key]
        public Guid RuleId { get; set; }

        [Key]
        public Guid ScenarioId { get; set; }

        [Required]
        public string ConfigurationKey { get; set; }

        [Required]
        public int ConfigurationValue { get; set; }

        public string? ActionTrigger { get; set; }

        [Required]
        public string RuleName { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public Guid DeviceId { get; set; }

        public string? APIKey { get; set; }

        public string? ApiValue { get; set; }
    }
}

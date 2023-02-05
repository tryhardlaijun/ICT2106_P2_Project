using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeManager.Domain.DirectorDomain.Entities;

public class RuleHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid RuleHistoryId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? ActionTrigger { get; set; }

    [Required] public int RuleNum { get; set; }

    [Required] public string ScenarioName { get; set; }

    [Required] public int ConfigurationValue { get; set; }

    [Required] public string DeviceName { get; set; }
}
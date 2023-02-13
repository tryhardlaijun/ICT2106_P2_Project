using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeManager.Domain.DirectorDomain.Entities;

public class RuleHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid RuleHistoryId { get; set; }

    [Required] public int RuleIndex { get; set; }

    [Required] public string RuleName { get; set; }

    public DateTime? RuleStartTime { get; set; }

    public DateTime? RuleEndTime { get; set; }

    public string? RuleActionTrigger { get; set; }

    [Required] public string ScenarioName { get; set; }

    [Required] public string DeviceName { get; set; }

    [Required] public string DeviceConfiguration { get; set; }
}
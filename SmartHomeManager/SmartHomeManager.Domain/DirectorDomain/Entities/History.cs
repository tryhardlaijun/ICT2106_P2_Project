using SmartHomeManager.Domain.AccountDomain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeManager.Domain.DirectorDomain.Entities
{
    public class History
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid HistoryId { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public string DeviceAdjustedConfiguration { get; set; }

        [Required]
        public Guid ProfileId { get; set; }

        [Required]
        public Guid RuleHistoryId { get; set; }

        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }

        [ForeignKey("RuleHistoryId")]
        public RuleHistory RuleHistory { get; set; }
    }
}

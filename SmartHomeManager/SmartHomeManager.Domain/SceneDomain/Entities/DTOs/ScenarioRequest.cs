using SmartHomeManager.Domain.DeviceDomain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeManager.Domain.SceneDomain.Entities.DTOs
{
	public class ScenarioRequest
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ScenarioId { get; set; }

        [Required]
        public String ScenarioName { get; set; } = default!;

        [Required]
        public Guid ProfileId { get; set; }

        [Required]
        public Boolean isActive { get; set; }
    }
}


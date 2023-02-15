using SmartHomeManager.Domain.AccountDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.EnergyProfileDomain.Entities
{
    public class EnergyProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EnergyProfileId { get; set; }

        [Required] public int ConfigurationValue { get; set; }

        [Required] public string ConfigurationDesc { get; set; }

        [Required] public Guid AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}

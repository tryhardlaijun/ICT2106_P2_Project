using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.HomeSecurityDomain.Entities
{
    public class HomeSecurity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid HomeSecurityId { get; set; }

        [Required] public bool SecurityModeState { get; set; }

        [Required] public Guid AccountId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.APIDomain.Entities
{
    public class APIData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid APIDataId { get; set; }

        [Required] public string Type { get; set; }

        [Required] public string Value { get; set; }

        [Required] public string Specification { get; set; }

        [Required] public DateTime TimeStamp { get; set; }
    }
}

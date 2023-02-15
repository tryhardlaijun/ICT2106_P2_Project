using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.APIDomain.Entities
{
    public class APIValue
    {
        [Key]
        public string APIKeyType { get; set; }

        public string? APIValues { get; set; }

        [ForeignKey("APIKeyType")]
        public APIKey APIKey { get; set; }
    }
}

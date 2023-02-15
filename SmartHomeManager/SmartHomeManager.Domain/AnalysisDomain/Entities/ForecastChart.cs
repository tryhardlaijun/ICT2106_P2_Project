using SmartHomeManager.Domain.AccountDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AnalysisDomain.Entities
{
    public class ForecastChart
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ForecastChartId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public int TimespanType { get; set; }

        [Required]
        public string DateOfAnalysis { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}

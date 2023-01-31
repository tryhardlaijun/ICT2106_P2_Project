using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeManager.Domain.SampleDomain.Entities
{
    public class Sample
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        public Sample(Guid id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}

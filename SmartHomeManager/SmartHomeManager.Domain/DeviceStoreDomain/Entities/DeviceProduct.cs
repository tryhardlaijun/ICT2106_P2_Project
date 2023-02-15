using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeManager.Domain.DeviceStoreDomain.Entities;

public class DeviceProduct
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public string Name { get; set; }

    [Required] public string Brand { get; set; }

    [Required] public string Model { get; set; }

    [Required] public string Type { get; set; }

    [Required] public string Description { get; set; }

    [Required] public float Price { get; set; }

    [Required] public int Quantity { get; set; }

    [Required] public string ImageUrl { get; set; }
}
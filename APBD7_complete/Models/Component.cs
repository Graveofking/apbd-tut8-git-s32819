using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD7.Models;

[Table("Components")]
public class Component
{
    [Key]
    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public int ComponentManufacturersId { get; set; }

    [ForeignKey(nameof(ComponentManufacturersId))]
    public ComponentManufacturer ComponentManufacturer { get; set; } = null!;

    public int ComponentTypesId { get; set; }

    [ForeignKey(nameof(ComponentTypesId))]
    public ComponentType ComponentType { get; set; } = null!;

    public ICollection<PCComponent> PCComponents { get; set; } = new List<PCComponent>();
}

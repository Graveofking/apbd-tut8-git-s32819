using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD7.Models;

[Table("ComponentManufacturers")]
public class ComponentManufacturer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Abbreviation { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public DateTime FoundationDate { get; set; }

    public ICollection<Component> Components { get; set; } = new List<Component>();
}

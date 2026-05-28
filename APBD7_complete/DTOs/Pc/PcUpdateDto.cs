using System.ComponentModel.DataAnnotations;

namespace APBD7.DTOs.Pc;

public class PcUpdateDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
}

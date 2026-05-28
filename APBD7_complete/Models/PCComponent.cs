using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD7.Models;

[Table("PCComponents")]
[PrimaryKey(nameof(PCId), nameof(ComponentCode))]
public class PCComponent
{
    public int PCId { get; set; }

    [ForeignKey(nameof(PCId))]
    public PC PC { get; set; } = null!;

    [Required]
    [MaxLength(10)]
    public string ComponentCode { get; set; } = string.Empty;

    [ForeignKey(nameof(ComponentCode))]
    public Component Component { get; set; } = null!;

    [Required]
    public int Amount { get; set; }
}

namespace APBD7.DTOs.Pc;

public class PcComponentDto
{
    public int Amount { get; set; }
    public ComponentDetailDto Component { get; set; } = null!;
}

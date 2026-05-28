namespace APBD7.DTOs.Pc;

public class ComponentDetailDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ComponentManufacturerDto Manufacturer { get; set; } = null!;
    public ComponentTypeDto Type { get; set; } = null!;
}

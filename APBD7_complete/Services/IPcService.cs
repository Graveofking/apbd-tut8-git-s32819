using APBD7.DTOs.Pc;

namespace APBD7.Services;

public interface IPcService
{
    Task<IEnumerable<PcGetDto>> GetAllPcsAsync();
    Task<PcGetDto?> GetPcByIdAsync(int id);
    Task<IEnumerable<PcComponentDto>> GetPcComponentsAsync(int pcId);
    Task<PcGetDto> CreatePcAsync(PcCreateDto dto);
    Task<PcGetDto?> UpdatePcAsync(int id, PcUpdateDto dto);
    Task<bool> DeletePcAsync(int id);
    Task<bool> PcExistsAsync(int id);
}

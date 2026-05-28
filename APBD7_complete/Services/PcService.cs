using Microsoft.EntityFrameworkCore;
using APBD7.Data;
using APBD7.DTOs.Pc;
using APBD7.Models;

namespace APBD7.Services;

public class PcService : IPcService
{
    private readonly AppDbContext _context;

    public PcService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PcGetDto>> GetAllPcsAsync()
    {
        return await _context.PCs
            .OrderBy(p => p.Id)
            .Select(p => MapToGetDto(p))
            .ToListAsync();
    }

    public async Task<PcGetDto?> GetPcByIdAsync(int id)
    {
        var pc = await _context.PCs.FindAsync(id);
        return pc == null ? null : MapToGetDto(pc);
    }

    public async Task<IEnumerable<PcComponentDto>> GetPcComponentsAsync(int pcId)
    {
        return await _context.PCComponents
            .Where(pc => pc.PCId == pcId)
            .Include(pc => pc.Component)
                .ThenInclude(c => c.ComponentManufacturer)
            .Include(pc => pc.Component)
                .ThenInclude(c => c.ComponentType)
            .Select(pc => new PcComponentDto
            {
                Amount = pc.Amount,
                Component = new ComponentDetailDto
                {
                    Code = pc.Component.Code,
                    Name = pc.Component.Name,
                    Description = pc.Component.Description,
                    Manufacturer = new ComponentManufacturerDto
                    {
                        Id = pc.Component.ComponentManufacturer.Id,
                        Abbreviation = pc.Component.ComponentManufacturer.Abbreviation,
                        FullName = pc.Component.ComponentManufacturer.FullName,
                        FoundationDate = pc.Component.ComponentManufacturer.FoundationDate
                    },
                    Type = new ComponentTypeDto
                    {
                        Id = pc.Component.ComponentType.Id,
                        Abbreviation = pc.Component.ComponentType.Abbreviation,
                        Name = pc.Component.ComponentType.Name
                    }
                }
            })
            .ToListAsync();
    }

    public async Task<PcGetDto> CreatePcAsync(PcCreateDto dto)
    {
        var pc = new PC
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };

        _context.PCs.Add(pc);
        await _context.SaveChangesAsync();

        return MapToGetDto(pc);
    }

    public async Task<PcGetDto?> UpdatePcAsync(int id, PcUpdateDto dto)
    {
        var pc = await _context.PCs.FindAsync(id);
        if (pc == null) return null;

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _context.SaveChangesAsync();

        return MapToGetDto(pc);
    }
    
    public async Task<bool> DeletePcAsync(int id)
    {
        var pc = await _context.PCs.FindAsync(id);
        if (pc == null) return false;

        _context.PCs.Remove(pc);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> PcExistsAsync(int id)
    {
        return await _context.PCs.AnyAsync(p => p.Id == id);
    }
    private static PcGetDto MapToGetDto(PC pc) => new()
    {
        Id = pc.Id,
        Name = pc.Name,
        Weight = pc.Weight,
        Warranty = pc.Warranty,
        CreatedAt = pc.CreatedAt,
        Stock = pc.Stock
    };
}

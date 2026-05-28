using Microsoft.AspNetCore.Mvc;
using APBD7.DTOs.Pc;
using APBD7.Services;

namespace APBD7.Controllers;

[ApiController]
[Route("api/pcs")]
public class PCsController : ControllerBase
{
    private readonly IPcService _pcService;

    public PCsController(IPcService pcService)
    {
        _pcService = pcService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPcs()
    {
        var pcs = await _pcService.GetAllPcsAsync();
        return Ok(pcs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPcById(int id)
    {
        var pc = await _pcService.GetPcByIdAsync(id);
        if (pc == null)
            return NotFound();
        return Ok(pc);
    }

    [HttpGet("{id}/components")]
    public async Task<IActionResult> GetPcComponents(int id)
    {
        if (!await _pcService.PcExistsAsync(id))
            return NotFound();

        var components = await _pcService.GetPcComponentsAsync(id);
        return Ok(components);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePc([FromBody] PcCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var pc = await _pcService.CreatePcAsync(dto);

        return CreatedAtAction(nameof(GetPcById), new { id = pc.Id }, pc);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePc(int id, [FromBody] PcUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var pc = await _pcService.UpdatePcAsync(id, dto);
        if (pc == null)
            return NotFound();

        return Ok(pc);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePc(int id)
    {
        var deleted = await _pcService.DeletePcAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}

using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Dtos;
using BlanquitaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace BlanquitaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CorteCajaController : ControllerBase
{
    private readonly CorteCajaService _corteCajaService;
    private readonly TacosBlanquitaContext _context;

    public CorteCajaController(CorteCajaService corteCajaService, TacosBlanquitaContext context)
    {
        _corteCajaService = corteCajaService;
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<CorteCaja>> GetCorteCajas()
    {
        return await _corteCajaService.GetCorteCajas();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CorteCaja>> GetCorteCaja(int id)
    {
        var corteCaja =  await _corteCajaService.GetCorteCaja(id);

        if(corteCaja == null)
        {
            return NotFound();
        }

        return corteCaja;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCorte(int id, CorteCajaDTO corteCaja)
    {
        if (id != corteCaja.IdCorteCaja)
        {
            return BadRequest();
        }

        var result = await _corteCajaService.PutCombo(id, corteCaja);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<CorteCaja>> PostCombo(CorteCajaDTO combo)
    {
        var createdCombo = await _corteCajaService.PostCombo(combo);
        return CreatedAtAction(nameof(GetCorteCaja), new { id = createdCombo.IdCorteCaja }, createdCombo);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCombo(int id)
    {
        var combo = await _corteCajaService.DeleteCombo(id);
        if (combo == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("ObtenerListado")]
    public async Task<IEnumerable<Object>> ObtenerListado(ObtenerListadoDTO obtener)
    {
        return await _corteCajaService.ObtenerListado(obtener.fecha);
    }

    [HttpPost("ObtenerListadoSuma")]
    public async Task<Object?> ObtenerListadoSuma(ObtenerListadoDTO obtener)
    {
        return await _corteCajaService.ObtenerListadoSuma(obtener.fecha);
    }
}


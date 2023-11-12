using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TipoProductoController : ControllerBase
{
    private readonly TipoProductoService _tipoProductoService;

    public TipoProductoController(TipoProductoService tipoProductoService)
    {
        _tipoProductoService = tipoProductoService;
    }

    // GET: api/TipoProducto
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoProducto>>> ConsultarTiposProductos()
    {
        return Ok(await _tipoProductoService.ConsultarTiposProductos());
    }

    // GET: api/TipoProducto/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TipoProducto>> ConsultarUnTipoProducto(int id)
    {
        var tipoProducto = await _tipoProductoService.ConsultarUnTipoProducto(id);

        if (tipoProducto == null)
        {
            return NotFound();
        }

        return tipoProducto;
    }

    // PUT: api/TipoProducto/5
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarTipoProducto(int id, TipoProducto tipoProducto)
    {
        var result = await _tipoProductoService.ActualizarTipoProducto(id, tipoProducto);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/TipoProducto
    [HttpPost]
    public async Task<ActionResult<TipoProducto>> Agregar()
    {
        var createdTipoProducto = await _tipoProductoService.Agregar();
        return CreatedAtAction(nameof(ConsultarUnTipoProducto), new { id = createdTipoProducto.IdTipoProducto }, createdTipoProducto);
    }

    // DELETE: api/TipoProducto/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTipoProducto(int id)
    {
        var tipoProducto = await _tipoProductoService.DeleteTipoProducto(id);
        if (tipoProducto == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlanquitaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private readonly OrdenService _ordenService;

        public OrdenController(OrdenService ordenService)
        {
            _ordenService = ordenService;
        }

        // GET: api/Orden
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orden>>> GetOrden()
        {
            return Ok(await _ordenService.GetOrdenes());
        }

        // GET: api/Orden/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orden>> GetOrden(int id)
        {
            var orden = await _ordenService.GetOrden(id);

            if (orden == null)
            {
                return NotFound();
            }

            return orden;
        }

        // PUT: api/Orden/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrden(int id, Orden orden)
        {
            if (id != orden.IdOrden)
            {
                return BadRequest();
            }

            var result = await _ordenService.PutOrden(id, orden);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Orden
        [HttpPost]
        public async Task<ActionResult<Orden>> PostOrden(Orden orden)
        {
            var createdOrden = await _ordenService.PostOrden(orden);
            return CreatedAtAction(nameof(GetOrden), new { id = createdOrden.IdOrden }, createdOrden);
        }

        // DELETE: api/Orden/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrden(int id)
        {
            var orden = await _ordenService.DeleteOrden(id);
            if (orden == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
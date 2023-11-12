
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace TacosBlanquitaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenComboController : ControllerBase
    {

        private readonly OrdenComboService _ordenComboService;

        public OrdenComboController(OrdenComboService ordenComboService)
        {
            _ordenComboService = ordenComboService;
        }
        // GET: api/OrdenCombo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenCombo>>> GetOrdenCombos()
        {
            return Ok(await _ordenComboService.GetOrdenCombos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenCombo>> GetOrdenCombo(int id)
        {
            var ordenCombo = await _ordenComboService.GetOrdenCombo(id);

            if (ordenCombo == null)
            {
                return NotFound();
            }

            return ordenCombo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdenCombo(int id, OrdenCombo ordenCombo)
        {
            if (id != ordenCombo.IdOrdenCombo)
            {
                return BadRequest();
            }

            var result = await _ordenComboService.PutOrdenCombo(id, ordenCombo);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<OrdenCombo>> PostOrdenCombo(OrdenCombo ordenCombo)
        {
            var createdOrdenCombo = await _ordenComboService.PostOrdenCombo(ordenCombo);
            return CreatedAtAction(nameof(GetOrdenCombo), new { id = createdOrdenCombo.IdOrdenCombo }, createdOrdenCombo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdenCombo(int id)
        {
            var ordenCombo = await _ordenComboService.DeleteOrdenCombo(id);
            if (ordenCombo == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}


using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Dtos;
using BlanquitaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlanquitaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private readonly TacosBlanquitaContext _context;
        private readonly ComboService _comboService;

        public ComboController(TacosBlanquitaContext context, ComboService comboService)
        {
            _context = context;
            _comboService = comboService;
        }

        // GET: api/Combo
        [HttpGet]
        public async Task<IEnumerable<Combo>> GetCombos()
        {
            return await _comboService.GetCombos();
        }

        // GET: api/Combo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Combo>> GetCombo(int id)
        {
            var combo = await _comboService.GetCombo(id);

            if (combo == null)
            {
                return NotFound();
            }

            return combo;
        }

        // PUT: api/Combo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCombo(int id, ComboDto combo)
        {
            if (id != combo.IdCombo)
            {
                return BadRequest();
            }

            var result = await _comboService.PutCombo(id, combo);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Combo>> PostCombo(ComboDto combo)
        {
            var createdCombo = await _comboService.PostCombo(combo);
            return CreatedAtAction(nameof(GetCombo), new { id = createdCombo.IdCombo }, createdCombo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCombo(int id)
        {
            var combo = await _comboService.DeleteCombo(id);
            if (combo == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

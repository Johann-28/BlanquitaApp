
using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlanquitaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private readonly TacosBlanquitaContext _context;

        public ComboController(TacosBlanquitaContext context)
        {
            _context = context;
        }

        // GET: api/Combo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Combo>>> GetCombos()
        {
            return await _context.Combo.ToListAsync();
        }

        // GET: api/Combo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Combo>> GetCombo(int id)
        {
            var combo = await _context.Combo.FindAsync(id);

            if (combo == null)
            {
                return NotFound();
            }

            return combo;
        }

        // PUT: api/Combo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCombo(int id, Combo combo)
        {
            if (id != combo.IdCombo)
            {
                return BadRequest();
            }

            _context.Entry(combo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComboExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Combo
        [HttpPost]
        public async Task<ActionResult<Combo>> PostCombo(Combo combo)
        {
            _context.Combo.Add(combo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCombo), new { id = combo.IdCombo }, combo);
        }

        // DELETE: api/Combo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCombo(int id)
        {
            var combo = await _context.Combo.FindAsync(id);
            if (combo == null)
            {
                return NotFound();
            }

            _context.Combo.Remove(combo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComboExists(int id)
        {
            return _context.Combo.Any(e => e.IdCombo == id);
        }
    }
}


using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TacosBlanquitaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenComboController : ControllerBase
    {
        private readonly TacosBlanquitaContext _context;

        public OrdenComboController(TacosBlanquitaContext context)
        {
            _context = context;
        }

        // GET: api/OrdenCombo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenCombo>>> GetOrdenCombos()
        {
            return await _context.OrdenCombo.ToListAsync();
        }

        // GET: api/OrdenCombo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenCombo>> GetOrdenCombo(int id)
        {
            var ordenCombo = await _context.OrdenCombo.FindAsync(id);

            if (ordenCombo == null)
            {
                return NotFound();
            }

            return ordenCombo;
        }

        // PUT: api/OrdenCombo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdenCombo(int id, OrdenCombo ordenCombo)
        {
            if (id != ordenCombo.IdOrdenCombo)
            {
                return BadRequest();
            }

            _context.Entry(ordenCombo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenComboExists(id))
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

        // POST: api/OrdenCombo
        [HttpPost]
        public async Task<ActionResult<OrdenCombo>> PostOrdenCombo(OrdenCombo ordenCombo)
        {
            _context.OrdenCombo.Add(ordenCombo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrdenCombo), new { id = ordenCombo.IdOrdenCombo }, ordenCombo);
        }

        // DELETE: api/OrdenCombo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdenCombo(int id)
        {
            var ordenCombo = await _context.OrdenCombo.FindAsync(id);
            if (ordenCombo == null)
            {
                return NotFound();
            }

            _context.OrdenCombo.Remove(ordenCombo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdenComboExists(int id)
        {
            return _context.OrdenCombo.Any(e => e.IdOrdenCombo == id);
        }
    }
}

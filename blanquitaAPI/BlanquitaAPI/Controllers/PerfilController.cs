
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BlanquitaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {

        private readonly TacosBlanquitaContext _context;
        public PerfilController(TacosBlanquitaContext context)
        {
            _context = context;
        }

        // GET: api/perfil
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Perfil>>> Get()
        {
            return await _context.Perfil.ToListAsync();
        }

        // GET: api/perfil/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Perfil>> Get(int id)
        {
            var perfil = await _context.Perfil.FirstOrDefaultAsync(p => p.IdPerfil == id);

            if (perfil == null)
            {
                return NotFound();
            }

            return perfil;
        }

        // POST: api/perfil
        [HttpPost]
        public async Task<ActionResult<Perfil>> Post()
        {
            Perfil perfil = new()
            {
                Clave = "ADM",
                Nombre = "Administrador",
            };

            _context.Perfil.Add(perfil);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = perfil.IdPerfil }, perfil);
        }


        // PUT: api/perfil/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Perfil perfil)
        {
            var existingPerfil = await _context.Perfil.FirstOrDefaultAsync(p => p.IdPerfil == id);

            if (existingPerfil == null)
            {
                return NotFound();
            }

            existingPerfil.Clave = perfil.Clave;
            existingPerfil.Nombre = perfil.Nombre;

            _context.Perfil.Update(existingPerfil);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/perfil/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var perfil = await _context.Perfil.FirstOrDefaultAsync(p => p.IdPerfil == id);

            if (perfil == null)
            {
                return NotFound();
            }

            _context.Perfil.Remove(perfil);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

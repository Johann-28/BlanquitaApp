using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProductoController : ControllerBase
    {
        private readonly TacosBlanquitaContext _context;

        public TipoProductoController(TacosBlanquitaContext context)
        {
            _context = context;
        }

        // GET: api/TipoProducto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProducto>>> ConsultarTiposProductos()
        {
            return await _context.TipoProducto
            .Include(tP => tP.Producto)
            .ToListAsync();
        }

        // GET: api/TipoProducto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoProducto>> ConsultarUnTipoProducto(int id)
        {
            var tipoProducto = await _context.TipoProducto.Include(tP => tP.Producto).FirstOrDefaultAsync(tP => tP.IdTipoProducto == id);

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
            if (id != tipoProducto.IdTipoProducto)
            {
                return BadRequest();
            }

            _context.Entry(tipoProducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoProductoExists(id))
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


        [HttpPost]
        public async Task<ActionResult<TipoProducto>> Agregar()
        {
            _context.TipoProducto.Add(new TipoProducto { Clave = "EJE", Descripcion = "Ejemplo" });
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoProducto(int id)
        {
            var tipoProducto = await _context.TipoProducto.FindAsync(id);
            if (tipoProducto == null)
            {
                return NotFound();
            }

            _context.TipoProducto.Remove(tipoProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoProductoExists(int id)
        {
            return _context.TipoProducto.Any(e => e.IdTipoProducto == id);
        }
    }
}
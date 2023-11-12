using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using Microsoft.EntityFrameworkCore;

namespace BlanquitaAPI.Services
{
    public class TipoProductoService
    {
        private readonly TacosBlanquitaContext _context;

        public TipoProductoService(TacosBlanquitaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoProducto>> ConsultarTiposProductos()
        {
            return await _context.TipoProducto.Include(tP => tP.Producto).ToListAsync();
        }

        public async Task<TipoProducto?> ConsultarUnTipoProducto(int id)
        {
            return await _context.TipoProducto.Include(tP => tP.Producto).FirstOrDefaultAsync(tP => tP.IdTipoProducto == id);
        }

        public async Task<bool> ActualizarTipoProducto(int id, TipoProducto tipoProducto)
        {
            if (id != tipoProducto.IdTipoProducto)
            {
                return false;
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
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<TipoProducto> Agregar()
        {
            var tipoProducto = new TipoProducto { Clave = "EJE", Descripcion = "Ejemplo" };
            _context.TipoProducto.Add(tipoProducto);
            await _context.SaveChangesAsync();
            return tipoProducto;
        }

        public async Task<TipoProducto?> DeleteTipoProducto(int id)
        {
            var tipoProducto = await _context.TipoProducto.FindAsync(id);
            if (tipoProducto == null)
            {
                return null;
            }

            _context.TipoProducto.Remove(tipoProducto);
            await _context.SaveChangesAsync();

            return tipoProducto;
        }

        private bool TipoProductoExists(int id)
        {
            return _context.TipoProducto.Any(e => e.IdTipoProducto == id);
        }
    }
}
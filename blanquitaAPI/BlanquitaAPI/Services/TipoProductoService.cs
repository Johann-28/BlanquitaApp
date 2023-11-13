using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Dtos;
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

        public async Task<bool> ActualizarTipoProducto(int id, TipoProductoDto tipoProducto)
        {
            if (id != tipoProducto.IdTipoProducto)
            {
                return false;
            }

            var tipoProductoToEdit = new TipoProducto
            {
                IdTipoProducto = tipoProducto.IdTipoProducto,
                Clave = tipoProducto.Clave,
                Descripcion = tipoProducto.Descripcion
            };

            _context.Entry(tipoProductoToEdit).State = EntityState.Modified;

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

        public async Task<TipoProducto> Agregar(TipoProductoDto tipoProducto)
        {
            var tipoProductoToCreate = new TipoProducto
            {
                Clave = tipoProducto.Clave,
                Descripcion = tipoProducto.Descripcion
            };
            _context.TipoProducto.Add(tipoProductoToCreate);
            await _context.SaveChangesAsync();
            return tipoProductoToCreate;
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
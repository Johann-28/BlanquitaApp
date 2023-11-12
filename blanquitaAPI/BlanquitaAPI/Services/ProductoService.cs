using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using Microsoft.EntityFrameworkCore;

namespace BlanquitaAPI.Services
{
    public class ProductoService
    {
        private readonly TacosBlanquitaContext _context;

        public ProductoService(TacosBlanquitaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> GetProductos()
        {
            return await _context.Producto.ToListAsync();
        }

        public async Task<Producto?> GetProducto(int id)
        {
            return await _context.Producto.FindAsync(id);
        }

        public async Task<Producto> PostProducto(Producto producto)
        {
            _context.Producto.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> PutProducto(int id, Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return false;
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        public async Task<Producto?> DeleteProducto(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return null;
            }

            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();

            return producto;
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.IdProducto == id);
        }
    }
}
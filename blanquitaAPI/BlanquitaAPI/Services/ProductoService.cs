using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Dtos;
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

        public async Task<Producto> PostProducto(ProductoDto producto)
        {
            var productoToCreate = new Producto
            {
                Descripcion = producto.Descripcion,
                IdProducto = producto.IdProducto,
                Precio = producto.Precio,
                IdTipoProducto = producto.IdTipoProducto
            };

            _context.Producto.Add(productoToCreate);
            await _context.SaveChangesAsync();
            return productoToCreate;
        }

        public async Task<bool> PutProducto(int id, ProductoDto producto)
        {
            if (id != producto.IdProducto)
            {
                return false;
            }

            var productoToEdit = new Producto
            {
                Descripcion = producto.Descripcion,
                IdProducto = producto.IdProducto,
                Precio = producto.Precio,
                IdTipoProducto = producto.IdTipoProducto
            };

            _context.Entry(productoToEdit).State = EntityState.Modified;

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
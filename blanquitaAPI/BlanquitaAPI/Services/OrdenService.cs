using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BlanquitaAPI.Services
{
    public class OrdenService
    {
        private readonly TacosBlanquitaContext _context;

        public OrdenService(TacosBlanquitaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Orden>> GetOrdenes()
        {
            return await _context.Orden.ToListAsync();
        }

        public async Task<Orden?> GetOrden(int id)
        {
            return await _context.Orden.FindAsync(id);
        }

        public async Task<bool> PutOrden(int id, OrdenDto orden)
        {
            if (id != orden.IdOrden)
            {
                return false;
            }

            Orden ordenToEdit = new()
            {
                IdOrden = orden.IdOrden,
                IdUsuario = orden.IdUsuario,
                Total = orden.Total,
                Fecha = orden.Fecha
            };

            _context.Entry(ordenToEdit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenExists(id))
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

        public async Task<Orden> PostOrden(OrdenDto orden)
        {
            Orden ordenToCreate = new()
            {
                IdOrden = orden.IdOrden,
                IdUsuario = orden.IdUsuario,
                Total = orden.Total,
                Fecha = orden.Fecha
            };
            _context.Orden.Add(ordenToCreate);
            await _context.SaveChangesAsync();
            return ordenToCreate;
        }

        public async Task<Orden?> DeleteOrden(int id)
        {
            var orden = await _context.Orden.FindAsync(id);
            if (orden == null)
            {
                return null;
            }

            _context.Orden.Remove(orden);
            await _context.SaveChangesAsync();

            return orden;
        }

        private bool OrdenExists(int id)
        {
            return _context.Orden.Any(e => e.IdOrden == id);
        }
    }
}
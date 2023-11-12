using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
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

        public async Task<bool> PutOrden(int id, Orden orden)
        {
            if (id != orden.IdOrden)
            {
                return false;
            }

            _context.Entry(orden).State = EntityState.Modified;

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

        public async Task<Orden> PostOrden(Orden orden)
        {
            _context.Orden.Add(orden);
            await _context.SaveChangesAsync();
            return orden;
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
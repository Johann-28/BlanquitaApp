using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BlanquitaAPI.Services
{
    public class OrdenComboService
    {
        private readonly TacosBlanquitaContext _context;

        public OrdenComboService(TacosBlanquitaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrdenCombo>> GetOrdenCombos()
        {
            return await _context.OrdenCombo.ToListAsync();
        }

        public async Task<OrdenCombo?> GetOrdenCombo(int id)
        {
            return await _context.OrdenCombo.FindAsync(id);
        }

        public async Task<bool> PutOrdenCombo(int id, OrdenComboDto ordenCombo)
        {
            if (id != ordenCombo.IdOrdenCombo)
            {
                return false;
            }

            OrdenCombo ordenComboToEdit = new()
            {
                IdOrdenCombo = ordenCombo.IdOrdenCombo,
                IdCombo = ordenCombo.IdCombo,
                IdOrden = ordenCombo.IdOrden
            };

            _context.Entry(ordenComboToEdit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenComboExists(id))
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

        public async Task<OrdenCombo> PostOrdenCombo(OrdenComboDto ordenCombo)
        {
            OrdenCombo ordenComboToCreate = new()
            {
                IdOrdenCombo = ordenCombo.IdOrdenCombo,
                IdCombo = ordenCombo.IdCombo,
                IdOrden = ordenCombo.IdOrden
            };
            _context.OrdenCombo.Add(ordenComboToCreate);
            await _context.SaveChangesAsync();
            return ordenComboToCreate;
        }

        public async Task<OrdenCombo?> DeleteOrdenCombo(int id)
        {
            var ordenCombo = await _context.OrdenCombo.FindAsync(id);
            if (ordenCombo == null)
            {
                return null;
            }

            _context.OrdenCombo.Remove(ordenCombo);
            await _context.SaveChangesAsync();

            return ordenCombo;
        }

        private bool OrdenComboExists(int id)
        {
            return _context.OrdenCombo.Any(e => e.IdOrdenCombo == id);
        }
    }
}
using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using Microsoft.EntityFrameworkCore;

namespace BlanquitaAPI.Services
{
    public class PerfilService
    {
        private readonly TacosBlanquitaContext _context;

        public PerfilService(TacosBlanquitaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Data.BlanquitaModels.Perfil>> GetPerfiles()
        {
            return await _context.Perfil.ToListAsync();
        }

        public async Task<Perfil?> GetPerfil(int id)
        {
            return await _context.Perfil.FirstOrDefaultAsync(p => p.IdPerfil == id);
        }

        public async Task<Perfil> PostPerfil(Perfil perfil)
        {
            _context.Perfil.Add(perfil);
            await _context.SaveChangesAsync();
            return perfil;
        }

        public async Task<bool> PutPerfil(int id, Perfil perfil)
        {
            var existingPerfil = await _context.Perfil.FirstOrDefaultAsync(p => p.IdPerfil == id);

            if (existingPerfil == null)
            {
                return false;
            }

            existingPerfil.Clave = perfil.Clave;
            existingPerfil.Nombre = perfil.Nombre;

            _context.Perfil.Update(existingPerfil);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Perfil?> DeletePerfil(int id)
        {
            var perfil = await _context.Perfil.FirstOrDefaultAsync(p => p.IdPerfil == id);
            if (perfil == null)
            {
                return null;
            }

            _context.Perfil.Remove(perfil);
            await _context.SaveChangesAsync();

            return perfil;
        }
    }
}
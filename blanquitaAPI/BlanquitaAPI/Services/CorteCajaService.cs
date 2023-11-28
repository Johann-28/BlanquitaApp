using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BlanquitaAPI.Services;

public class CorteCajaService
{
    private readonly TacosBlanquitaContext _context;

    public CorteCajaService(TacosBlanquitaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CorteCaja>> GetCorteCajas()
    {
        return await _context.CorteCaja.ToListAsync();
    }

    public async Task<CorteCaja?> GetCorteCaja(int id)
    {
        return await _context.CorteCaja.FindAsync(id);
    }

    public async Task<bool> PutCombo(int id, CorteCajaDTO combo)
    {
        if (id != combo.IdCorteCaja)
        {
            return false;
        }


        CorteCaja comboToEdit = new()
        {
            SaldoInicial = combo.SaldoInicial,
            SaldoFinal = combo.SaldoFinal,
            IdCorteCaja = combo.IdCorteCaja,
            IdUsuario = combo.IdUsuario,
            Fecha = combo.Fecha,
            Comentarios = combo.Comentarios,
        };

        _context.Entry(comboToEdit).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ComboExists(id))
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



    private bool ComboExists(int id)
    {
        return _context.CorteCaja.Any(e => e.IdCorteCaja == id);
    }

    public async Task<CorteCaja> PostCombo(CorteCajaDTO combo)
    {
        var comboToCreate = new CorteCaja
        {
            Comentarios = combo.Comentarios,
            SaldoFinal = combo.SaldoFinal,
            SaldoInicial = combo.SaldoInicial,
            Fecha = combo.Fecha,
            IdUsuario = combo.IdUsuario,
            IdCorteCaja = combo.IdCorteCaja
        };
        _context.CorteCaja.Add(comboToCreate);
        await _context.SaveChangesAsync();
        return comboToCreate;
    }

    public async Task<CorteCaja?> DeleteCombo(int id)
    {
        var combo = await _context.CorteCaja.FindAsync(id);
        if (combo == null)
        {
            return null;
        }

        _context.CorteCaja.Remove(combo);
        await _context.SaveChangesAsync();

        return combo;
    }

    public async Task<IEnumerable<Object>> ObtenerListado(DateTime fecha)
    {
        return await _context.Orden
                .GroupBy(o => new { o.IdUsuario, o.Fecha })
                .Where(o => o.Key.Fecha == fecha)
                .Select(o => new {
                            Total = o.Sum(x => x.Total),
                            Nombre =  _context.Usuario.Where(x => x.IdUsuario == o.Key.IdUsuario).FirstOrDefault().Nombre,
                            Fecha = o.Key.Fecha
                        })
                .ToListAsync();
                
    }

    public async Task<Object?> ObtenerListadoSuma(DateTime fecha)
    {
        return await _context.Orden
                .GroupBy(o => new { o.Fecha })
                .Where(o => o.Key.Fecha == fecha)
                .Select(o => new {
                    Total = o.Sum(x => x.Total),
                })
                .FirstOrDefaultAsync();

    }
}


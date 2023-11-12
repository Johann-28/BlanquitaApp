
using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using Microsoft.EntityFrameworkCore;

public class ComboService
{
    private readonly TacosBlanquitaContext _context;

    public ComboService(TacosBlanquitaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Combo>> GetCombos()
    {
        return await _context.Combo.ToListAsync();
    }

    public async Task<Combo?> GetCombo(int id)
    {
        return await _context.Combo.FindAsync(id);
    }

    public async Task<bool> PutCombo(int id, Combo combo)
    {
        if (id != combo.IdCombo)
        {
            return false;
        }

        _context.Entry(combo).State = EntityState.Modified;

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
        return _context.Combo.Any(e => e.IdCombo == id);
    }

    public async Task<Combo> PostCombo(Combo combo)
    {
        _context.Combo.Add(combo);
        await _context.SaveChangesAsync();
        return combo;
    }

    public async Task<Combo?> DeleteCombo(int id)
    {
        var combo = await _context.Combo.FindAsync(id);
        if (combo == null)
        {
            return null;
        }

        _context.Combo.Remove(combo);
        await _context.SaveChangesAsync();

        return combo;
    }




}

using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BlanquitaAPI.Services;

public class ProductoComboService
{
    private readonly TacosBlanquitaContext _context;

    public ProductoComboService(TacosBlanquitaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductoCombo>> GetProductoCombos()
    {
        return await _context.ProductoCombo.ToListAsync();
    }

    public async Task<IEnumerable<ProductoCombo>> GetProductoCombo(int id)
    {
        return await _context.ProductoCombo.Where(x => x.IdCombo == id).ToListAsync();
    }

    public async Task<bool> PutProductoCombo(int id, ProductoComboDto productoComboDTO)
    {
        if (id != productoComboDTO.IdProductoCombo)
        {
            return false;
        }


        ProductoCombo productoComboToEdit = new()
        {
            IdProductoCombo = productoComboDTO.IdProductoCombo,
            IdProducto = productoComboDTO.IdProducto,
            IdCombo = productoComboDTO.IdCombo,
        };

        _context.Entry(productoComboToEdit).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductoComboExists(id))
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

    private bool ProductoComboExists(int id)
    {
        return _context.ProductoCombo.Any(e => e.IdProductoCombo == id);
    }

    public async Task<ProductoCombo> PostCombo(ProductoComboDto combo)
    {
        var comboToCreate = new ProductoCombo
        {
            IdProducto = combo.IdProducto,
            IdCombo = combo.IdCombo
        };
        _context.ProductoCombo.Add(comboToCreate);
        await _context.SaveChangesAsync();
        return comboToCreate;
    }

    public async Task<IEnumerable<ProductoCombo?>> DeleteProductoComboPorCombo(int id)
    {
        var combo = await _context.ProductoCombo.Where(x => x.IdCombo == id).ToListAsync();
        if (combo == null)
        {
            return null;
        }

        _context.ProductoCombo.RemoveRange(combo);
        await _context.SaveChangesAsync();

        return combo;
    }

    public async Task<IEnumerable<ProductoCombo?>> DeleteProductoComboPorComboyProducto(int idProducto, int idCombo)
    {
        var combo = await _context.ProductoCombo.Where(x => x.IdCombo == idCombo && x.IdProducto == idProducto ).ToListAsync();
        if (combo == null)
        {
            return null;
        }

        _context.ProductoCombo.RemoveRange(combo);
        await _context.SaveChangesAsync();

        return combo;
    }

}


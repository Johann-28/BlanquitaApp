using System;
using System.Collections.Generic;

namespace BlanquitaAPI.Data.BlanquitaModels;

public partial class Producto
{
    public int IdProducto { get; set; }

    public int IdTipoProducto { get; set; }

    public string? Descripcion { get; set; }

    public double Precio { get; set; }

    public int? IdCombo { get; set; }

    public virtual Combo? IdComboNavigation { get; set; }

    public virtual TipoProducto IdTipoProductoNavigation { get; set; } = null!;
}

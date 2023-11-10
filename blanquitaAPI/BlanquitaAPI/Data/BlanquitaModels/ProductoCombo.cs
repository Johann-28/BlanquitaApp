using System;
using System.Collections.Generic;

namespace BlanquitaAPI.Data.BlanquitaModels;

public partial class ProductoCombo
{
    public int IdProductoCombo { get; set; }

    public int IdProducto { get; set; }

    public int IdCombo { get; set; }

    public virtual Combo IdComboNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}

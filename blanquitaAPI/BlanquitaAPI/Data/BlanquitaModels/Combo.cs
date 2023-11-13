using System;
using System.Collections.Generic;

namespace BlanquitaAPI.Data.BlanquitaModels;

public partial class Combo
{
    public int IdCombo { get; set; }

    public string? Descripcion { get; set; }

    public double? Total { get; set; }

    public virtual ICollection<OrdenCombo> OrdenCombo { get; set; } = new List<OrdenCombo>();

    public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
}

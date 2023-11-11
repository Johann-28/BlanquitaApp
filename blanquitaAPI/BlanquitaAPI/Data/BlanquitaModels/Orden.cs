using System;
using System.Collections.Generic;

namespace BlanquitaAPI.Data.BlanquitaModels;

public partial class Orden
{
    public int IdOrden { get; set; }

    public int IdUsuario { get; set; }

    public double Total { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<OrdenCombo> OrdenCombo { get; set; } = new List<OrdenCombo>();
}

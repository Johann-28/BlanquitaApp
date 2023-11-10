using System;
using System.Collections.Generic;

namespace BlanquitaAPI.Data.BlanquitaModels;

public partial class OrdenCombo
{
    public int IdOrdenCombo { get; set; }

    public int IdOrden { get; set; }

    public int IdCombo { get; set; }

    public virtual Combo IdComboNavigation { get; set; } = null!;

    public virtual Orden IdOrdenNavigation { get; set; } = null!;
}

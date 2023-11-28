using System;
using System.Collections.Generic;

namespace BlanquitaAPI.Data.BlanquitaModels;

public partial class CorteCaja
{
    public int IdCorteCaja { get; set; }

    public int IdUsuario { get; set; }

    public double SaldoInicial { get; set; }

    public double SaldoFinal { get; set; }

    public DateTime Fecha { get; set; }

    public string? Comentarios { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}

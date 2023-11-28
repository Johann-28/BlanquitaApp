using System;
using System.Collections.Generic;

namespace BlanquitaAPI.Data.BlanquitaModels;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int IdPerfil { get; set; }

    public virtual ICollection<CorteCaja> CorteCaja { get; set; } = new List<CorteCaja>();

    public virtual Perfil IdPerfilNavigation { get; set; } = null!;

    public virtual ICollection<Orden> Orden { get; set; } = new List<Orden>();
}

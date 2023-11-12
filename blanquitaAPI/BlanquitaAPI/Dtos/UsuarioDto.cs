namespace BlanquitaAPI.Dtos;
public class UsuarioDTO
{
    public int IdUsuario { get; set; }
    public string Nombre { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public int IdPerfil { get; set; }
    public string Contrasena { get; set; } = null!;
}
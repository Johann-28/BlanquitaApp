
using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Dtos;
using Microsoft.EntityFrameworkCore;

public class UsuarioService
{
    private readonly TacosBlanquitaContext _context;

    public UsuarioService(TacosBlanquitaContext context)
    {
        _context = context;
    }


    public bool UsuarioExists(int id)
    {
        return _context.Usuario.Any(e => e.IdUsuario == id);
    }

    public Usuario? Login(LoginRequest loginRequest)
    {
        Usuario? usuario = _context.Usuario
                                    .Include(us => us.IdPerfilNavigation)
                                    .FirstOrDefault(u =>
                                    u.Correo == loginRequest.Correo &&
                                    u.Contrasena == loginRequest.Password);

        return usuario;
    }

}

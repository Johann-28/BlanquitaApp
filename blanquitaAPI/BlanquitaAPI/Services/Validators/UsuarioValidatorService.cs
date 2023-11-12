
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Dtos;
using BlanquitaAPI.Services;

public class UsuarioValidatorService
{
    private readonly EncryptionService _encryptionService;
    private readonly UsuarioService _usuarioService;
    public UsuarioValidatorService(EncryptionService encryptionService, UsuarioService usuarioService)
    {
        _encryptionService = encryptionService;
        _usuarioService = usuarioService;
    }

    public Usuario ValidateUserExists(LoginRequest loginRequest)
    {
        string encryptedPassword = _encryptionService.EncryptToString(loginRequest.Password);
        loginRequest.Password = encryptedPassword;
        Usuario? usuario = _usuarioService.Login(loginRequest);

        if (usuario == null)
        {
            throw Exception("Usuario no encontrado");
        }
        return usuario;
    }

    private Exception Exception(string v)
    {
        throw new NotImplementedException();
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace BlanquitaAPI.Services;

public class LoginService
{
    private readonly EncryptionService _helpersService;
    private readonly UsuarioValidatorService _usuarioValidatorService;
    private readonly IConfiguration _config;

    public LoginService(EncryptionService helpersService, UsuarioValidatorService usuarioValidatorService, IConfiguration config)
    {
        _helpersService = helpersService;
        _usuarioValidatorService = usuarioValidatorService;
        _config = config;
    }

    public LoginResponse Authenticate(LoginRequest loginRequest)
    {

        Usuario usuario = _usuarioValidatorService.ValidateUserExists(loginRequest);
        string encryptedPassword = _helpersService.EncryptToString(loginRequest.Password);

        string password = _helpersService.DecryptString(encryptedPassword);
        string token = GenerateToken(usuario);
        return new LoginResponse
        {
            Token = token
        };
    }



    private string GenerateToken(Usuario usuario)
    {
        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Role , usuario.IdPerfilNavigation.Clave)
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds
        );

        string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }

}

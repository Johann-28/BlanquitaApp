namespace BlanquitaAPI.Dtos;

public class LoginRequest
{
    //generate a mock login model to copy in postman 
    public string Correo { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
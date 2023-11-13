
using BlanquitaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using BlanquitaAPI.Dtos;

namespace BlanquitaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly EncryptionService _helpersService;
        private readonly LoginService _loginService;

        public LoginController(IConfiguration config, EncryptionService helpersService, LoginService loginService)
        {
            _config = config;
            _helpersService = helpersService;
            _loginService = loginService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(LoginRequest loginRequest)
        {
            try
            {
                var loginResult = _loginService.Authenticate(loginRequest);
                return Ok(loginResult);

            }
            catch
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

        }

    }


}

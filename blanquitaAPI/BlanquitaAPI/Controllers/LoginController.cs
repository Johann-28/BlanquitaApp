
using BlanquitaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using BlanquitaAPI.Dtos;
using Microsoft.VisualBasic;

namespace BlanquitaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet("register")]
        public IActionResult Register()
        {
            var contrasenaMaria = "237007133105057224156062119094204213144203139120";
            var contrasenaMariaDesencriptada = _helpersService.DecryptString(contrasenaMaria);
            return Ok(contrasenaMariaDesencriptada);

         }

    }
}

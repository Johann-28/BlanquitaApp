using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlanquitaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly PerfilService _perfilService;

        public PerfilController(PerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        // GET: api/perfil
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Perfil>>> Get()
        {
            return Ok(await _perfilService.GetPerfiles());
        }

        // GET: api/perfil/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Perfil>> Get(int id)
        {
            var perfil = await _perfilService.GetPerfil(id);

            if (perfil == null)
            {
                return NotFound();
            }

            return perfil;
        }

        // POST: api/perfil
        [HttpPost]
        public async Task<ActionResult<Perfil>> Post([FromBody] Perfil perfil)
        {
            var createdPerfil = await _perfilService.PostPerfil(perfil);
            return CreatedAtAction(nameof(Get), new { id = createdPerfil.IdPerfil }, createdPerfil);
        }

        // PUT: api/perfil/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Perfil perfil)
        {
            var result = await _perfilService.PutPerfil(id, perfil);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/perfil/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var perfil = await _perfilService.DeletePerfil(id);
            if (perfil == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
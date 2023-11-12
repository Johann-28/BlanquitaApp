
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Data;
using BlanquitaAPI.Services;
using BlanquitaAPI.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace BlanquitaAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly TacosBlanquitaContext _context;
        private readonly EncryptionService _helpersService;
        private readonly UsuarioService _usuarioService;

        public UsuarioController(TacosBlanquitaContext context, EncryptionService helpersService, UsuarioService usuarioService)
        {
            _context = context;
            _helpersService = helpersService;
            _usuarioService = usuarioService;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuario.ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioDTO usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }

            try
            {
                var usuarioToUpdate = new Usuario();
                usuarioToUpdate = await _context.Usuario.FindAsync(id);
                usuarioToUpdate.Nombre = usuario.Nombre;
                usuarioToUpdate.Correo = usuario.Correo;
                usuarioToUpdate.IdPerfil = usuario.IdPerfil;
                usuarioToUpdate.Contrasena = _helpersService.EncryptToString(usuario.Contrasena);

                _context.Entry(usuarioToUpdate).State = EntityState.Modified;



                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_usuarioService.UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario()
        {

            var usuario = new Usuario
            {
                Nombre = "Juan",
                Contrasena = "123456",
                IdPerfil = 1,
                Correo = "johann"

            };
            usuario.Contrasena = _helpersService.EncryptToString(usuario.Contrasena);

            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}

using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Dtos;
using BlanquitaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlanquitaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoComboController : ControllerBase
    {
        private readonly TacosBlanquitaContext _context;
        private readonly ProductoComboService _productoComboService;

        public ProductoComboController(TacosBlanquitaContext context, ProductoComboService productoComboService)
        {
            _context = context;
           _productoComboService = productoComboService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductoCombo>> GetProductoCombos()
        {
            return await _productoComboService.GetProductoCombos();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductoCombo>>> GetProductoCombo(int id)
        {
            var combo = await _productoComboService.GetProductoCombo(id);

            if (combo == null)
            {
                return NotFound();
            }

            return Ok(combo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductoCombo(int id, ProductoComboDto combo)
        {
            if (id != combo.IdCombo)
            {
                return BadRequest();
            }

            var result = await _productoComboService.PutProductoCombo(id, combo);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ProductoCombo>> PostProductoCombo(ProductoComboDto combo)
        {
            var createdCombo = await _productoComboService.PostCombo(combo);
            return CreatedAtAction(nameof(GetProductoCombo), new { id = createdCombo.IdCombo }, createdCombo);
        }

        [HttpDelete("PorCombo/{id}")]
        public async Task<IActionResult> DeleteComboPorCombo(int id)
        {
            var combo = await _productoComboService.DeleteProductoComboPorCombo(id);
            if (combo == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("PorComboyProducto")]
        public async Task<IActionResult> DeleteComboPorComboProducto(ProductoComboDto productoComboDto)
        {
            var combo = await _productoComboService.DeleteProductoComboPorComboyProducto(productoComboDto.IdProducto,productoComboDto.IdCombo);
            if (combo == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

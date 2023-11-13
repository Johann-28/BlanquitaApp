using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Dtos;
using BlanquitaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlanquitaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _productoService;

        public ProductoController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return Ok(await _productoService.GetProductos());
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _productoService.GetProducto(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // POST: api/Producto
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto([FromBody] ProductoDto producto)
        {
            var createdProducto = await _productoService.PostProducto(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = createdProducto.IdProducto }, createdProducto);
        }

        // PUT: api/Producto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, ProductoDto producto)
        {
            var result = await _productoService.PutProducto(id, producto);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _productoService.DeleteProducto(id);
            if (producto == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
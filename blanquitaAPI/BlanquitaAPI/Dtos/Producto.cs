
namespace BlanquitaAPI.Dtos
{
    public class ProductoDto
    {
        public int IdProducto { get; set; }
        public int IdTipoProducto { get; set; }
        public string? Descripcion { get; set; }
        public double Precio { get; set; }
    }
}

namespace BlanquitaAPI.Dtos
{
    public class OrdenDto
    {
        public int IdOrden { get; set; }
        public int IdUsuario { get; set; }
        public double Total { get; set; }
        public DateTime Fecha { get; set; }
    }
}
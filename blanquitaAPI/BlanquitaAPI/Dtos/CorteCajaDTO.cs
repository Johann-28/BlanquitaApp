namespace BlanquitaAPI.Dtos;

public class CorteCajaDTO
{
    public int IdCorteCaja {  get; set; }
    public int IdUsuario { get; set; }
    public float SaldoInicial { get; set; }
    public float SaldoFinal{ get; set; }
    public DateTime Fecha { get; set; }
    public string Comentarios { get; set; }
}


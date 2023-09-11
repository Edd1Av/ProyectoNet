namespace WebApi.Models
{
    public class FacturaApiModel
    {
        public int Id { get; set; }
        public int? IdCliente { get; set; }
        public string? Folio { get; set; }
        public float? Saldo { get; set; }
        public DateTime? FechaFacturacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}

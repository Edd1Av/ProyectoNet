using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Domain
{
    public class Factura
    {
        public int Id { get; set; }
        [Required]
        public int IdCliente { get; set; }
        [Required]
        [StringLength(10)]
        public string? Folio { get; set; }
        [Required]
        public decimal Saldo { get; set; }
        [Required]
        public DateTime FechaFacturacion { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }
    }
}

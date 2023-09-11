using System.ComponentModel.DataAnnotations;

namespace Domain.Domain
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string? Apellido { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        [StringLength(100)]
        public string? CorreoElectronico { get; set; }

        public List<Factura> Facturas { get; set; }
    }
}

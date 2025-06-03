using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogisticaBackend.Models
{
    [Table("facturas")]
    public class Factura
    {
        [Key]
        [Column("id_factura")]
        public int IdFactura { get; set; }

        [Required]
        [Column("id_envio")]
        public int IdEnvio { get; set; }

        [Required]
        [StringLength(20)]
        [Column("numero_factura")]
        public string NumeroFactura { get; set; } = string.Empty;

        [Required]
        [Column("fecha_emision")]
        public DateTime FechaEmision { get; set; }

        [Required]
        [Column("monto_total", TypeName = "decimal(12,2)")]
        public decimal MontoTotal { get; set; }

        [StringLength(50)]
        [Column("metodo_pago")]
        public string? MetodoPago { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; } = false;

        [Required]
        [Column("id_estado_factura")]
        public int IdEstadoFactura { get; set; } = 1;

        // Navegación
        [ForeignKey("IdEnvio")]
        public virtual Envio Envio { get; set; } = null!;

        [ForeignKey("IdEstadoFactura")]
        public virtual EstadoFactura EstadoFactura { get; set; } = null!;
    }
}

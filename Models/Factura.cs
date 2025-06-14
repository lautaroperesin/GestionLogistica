using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GestionLogisticaBackend.Models;

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
        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(50)]
        [Column("numero_factura")]
        public string NumeroFactura { get; set; } = string.Empty;

        [Required]
        [Column("fecha_emision")]
        public DateTime FechaEmision { get; set; }

        [Column("fecha_vencimiento")]
        public DateTime? FechaVencimiento { get; set; }

        [Required]
        [Column("subtotal", TypeName = "decimal(12,2)")]
        public decimal Subtotal { get; set; }

        [Required]
        [Column("iva", TypeName = "decimal(12,2)")]
        public decimal Iva { get; set; }

        [Required]
        [Column("total", TypeName = "decimal(12,2)")]
        public decimal Total { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; } = false;

        [Required]
        [Column("id_estado_factura")]
        public int IdEstadoFactura { get; set; } = 1;

        // Navegación
        [ForeignKey("IdEnvio")]
        public virtual Envio Envio { get; set; } = null!;

        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; } = null!;

        [ForeignKey("IdEstadoFactura")]
        public virtual EstadoFactura EstadoFactura { get; set; } = null!;

        public virtual ICollection<MovimientoCaja> MovimientosCaja { get; set; } = new List<MovimientoCaja>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GestionLogisticaBackend.Enums;
using LogisticaBackend.Models;

namespace GestionLogisticaBackend.Models
{
    [Table("movimientos_caja")]
    public class MovimientoCaja
    {
        [Key]
        [Column("id_movimiento")]
        public int IdMovimiento { get; set; }

        [Required]
        [Column("id_factura")]
        public int IdFactura { get; set; }

        [Required]
        [Column("fecha_pago")]
        public DateTime FechaPago { get; set; } = DateTime.Now;

        [Required]
        [Column("monto")]
        public decimal Monto { get; set; }

        [Required]
        [Column("metodo_pago")]
        public MetodoPagoEnum MetodoPago { get; set; } = MetodoPagoEnum.NoDefinido;

        [Column("observaciones")]
        public string? Observaciones { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; } = false;

        // Navegación
        [ForeignKey("IdFactura")]
        public virtual Factura Factura { get; set; } = null!;
    }
}

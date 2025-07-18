using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogisticaBackend.Models
{
    [Table("envios")]
    public class Envio
    {
        [Key]
        [Column("id_envio")]
        public int IdEnvio { get; set; }

        [Required]
        [Column("id_origen")]
        public int IdOrigen { get; set; }

        [Required]
        [Column("id_destino")]
        public int IdDestino { get; set; }

        [Required]
        [StringLength(50)]
        [Column("numero_seguimiento")]
        public string NumeroSeguimiento { get; set; } = string.Empty;

        [Required]
        [Column("fecha_creacion_envio")]
        public DateTime FechaCreacionEnvio { get; set; } = DateTime.Now;

        [Column("fecha_salida")]
        public DateTime? FechaSalida { get; set; }

        [Required]
        [Column("id_estado_envio")]
        public int IdEstado { get; set; }

        [Required]
        [Column("peso_kg", TypeName = "decimal(10,2)")]
        public decimal PesoKg { get; set; }

        [Required]
        [Column("volumen_m3", TypeName = "decimal(10,2)")]
        public decimal VolumenM3 { get; set; }

        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Column("costo_total", TypeName = "decimal(10,2)")]
        public decimal CostoTotal { get; set; } = 0.00m;

        [Required]
        [Column("id_vehiculo")]
        public int IdVehiculo { get; set; }

        [Required]
        [Column("id_conductor")]
        public int IdConductor { get; set; }

        [Required]
        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Required]
        [Column("id_tipo_carga")]
        public int IdTipoCarga { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; } = false;

        // Navegación
        [ForeignKey("IdOrigen")]
        public virtual Ubicacion Origen { get; set; } = null!;

        [ForeignKey("IdDestino")]
        public virtual Ubicacion Destino { get; set; } = null!;

        [ForeignKey("IdEstado")]
        public virtual EstadoEnvio Estado { get; set; } = null!;

        [ForeignKey("IdVehiculo")]
        public virtual Vehiculo Vehiculo { get; set; } = null!;

        [ForeignKey("IdConductor")]
        public virtual Conductor Conductor { get; set; } = null!;

        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; } = null!;

        [ForeignKey("IdTipoCarga")]
        public virtual TipoCarga TipoCarga { get; set; } = null!;

        public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }
}

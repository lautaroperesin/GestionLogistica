using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GestionLogisticaBackend.Enums;

namespace LogisticaBackend.Models
{
    [Table("vehiculos")]
    public class Vehiculo
    {
        [Key]
        [Column("id_vehiculo")]
        public int IdVehiculo { get; set; }

        [Required]
        [StringLength(10)]
        [Column("patente")]
        public string Patente { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("marca")]
        public string Marca { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("modelo")]
        public string Modelo { get; set; } = string.Empty;

        [Required]
        [Column("capacidad_kg", TypeName = "decimal(10,2)")]
        public decimal CapacidadKg { get; set; }

        [Required]
        [Column("estado_vehiculo")]
        public EstadoVehiculoEnum Estado { get; set; } = EstadoVehiculoEnum.Disponible;

        [Required]
        [Column("ultima_inspeccion")]
        public DateTime UltimaInspeccion { get; set; }

        [Column("rto_vencimiento")]
        public DateTime RtoVencimiento { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; } = false;

        // Navegación
        public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();
    }
}

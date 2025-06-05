using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogisticaBackend.Models
{
    [Table("conductores")]
    public class Conductor
    {
        [Key]
        [Column("id_conductor")]
        public int IdConductor { get; set; }

        [Required]
        [StringLength(20)]
        [Column("dni")]
        public string Dni { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("conductor")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        [Column("clase_licencia")]
        public string ClaseLicencia { get; set; } = string.Empty;

        [Required]
        [Column("vencimiento_licencia")]
        public DateTime VencimientoLicencia { get; set; }

        [Required]
        [StringLength(20)]
        [Column("telefono")]
        public string Telefono { get; set; } = string.Empty;

        [StringLength(100)]
        [Column("email")]
        public string? Email { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; } = false;

        // Navegación
        public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();
    }
}

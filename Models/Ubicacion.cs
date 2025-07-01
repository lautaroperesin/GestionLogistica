using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogisticaBackend.Models
{
    [Table("ubicaciones")]
    public class Ubicacion
    {
        [Key]
        [Column("id_ubicacion")]
        public int IdUbicacion { get; set; }

        [StringLength(100)]
        [Column("direccion")]
        public string? Direccion { get; set; } = string.Empty;

        [StringLength(100)]
        [Column("descripcion")]
        public string? Descripcion { get; set; } = string.Empty;

        [Required]
        [Column("id_localidad")]
        public int IdLocalidad { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; } = false;

        // Navegación
        [ForeignKey("IdLocalidad")]
        public virtual Localidad Localidad { get; set; } = null!;

        public virtual ICollection<Envio> EnviosOrigen { get; set; } = new List<Envio>();
        public virtual ICollection<Envio> EnviosDestino { get; set; } = new List<Envio>();
    }
}

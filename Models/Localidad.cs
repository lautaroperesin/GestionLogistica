using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogisticaBackend.Models
{
    [Table("localidades")]
    public class Localidad
    {
        [Key]
        [Column("id_localidad")]
        public int IdLocalidad { get; set; }

        [Required]
        [StringLength(50)]
        [Column("localidad")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column("id_provincia")]
        public int IdProvincia { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; } = false;

        // Navegación
        [ForeignKey("IdProvincia")]
        public virtual Provincia Provincia { get; set; } = null!;
        public virtual ICollection<Ubicacion> Ubicaciones { get; set; } = new List<Ubicacion>();
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogisticaBackend.Models
{
    [Table("provincias")]
    public class Provincia
    {
        [Key]
        [Column("id_provincia")]
        public int IdProvincia { get; set; }

        [Required]
        [StringLength(50)]
        [Column("provincia")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column("id_pais")]
        public int IdPais { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; } = false;

        // Navegación
        [ForeignKey("IdPais")]
        public virtual Pais Pais { get; set; } = null!;
        public virtual ICollection<Localidad> Localidades { get; set; } = new List<Localidad>();
    }
}

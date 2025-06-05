using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogisticaBackend.Models
{
    [Table("paises")]
    public class Pais
    {
        [Key]
        [Column("id_pais")]
        public int IdPais { get; set; }

        [Required]
        [StringLength(50)]
        [Column("pais")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(2)]
        [Column("codigo_iso")]
        public string? CodigoIso { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; } = false;

        // Navegación
        public virtual ICollection<Provincia> Provincias { get; set; } = new List<Provincia>();
    }
}

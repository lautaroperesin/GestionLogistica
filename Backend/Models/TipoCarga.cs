using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogisticaBackend.Models
{
    [Table("tipos_carga")]
    public class TipoCarga
    {
        [Key]
        [Column("id_tipo_carga")]
        public int IdTipoCarga { get; set; }

        [Required]
        [StringLength(50)]
        [Column("carga")]
        public string Nombre { get; set; } = string.Empty;

        // Navegación
        public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();
    }
}

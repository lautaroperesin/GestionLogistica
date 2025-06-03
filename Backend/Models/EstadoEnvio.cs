using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogisticaBackend.Models
{
    [Table("estados_envio")]
    public class EstadoEnvio
    {
        [Key]
        [Column("id_estado")]
        public int IdEstado { get; set; }

        [Required]
        [StringLength(40)]
        [Column("estado")]
        public string Nombre { get; set; } = string.Empty;

        // Navegación
        public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();
    }
}

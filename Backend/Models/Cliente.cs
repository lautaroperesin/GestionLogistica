using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogisticaBackend.Models
{
    [Table("clientes")]
    public class Cliente
    {
        [Key]
        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(100)]
        [Column("cliente")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("telefono")]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("deleted")]
        public bool Deleted { get; set; } = false;

        // Navegación
        public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();
    }
}

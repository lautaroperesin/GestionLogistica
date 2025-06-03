using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogisticaBackend.Models
{
    [Table("estados_factura")]
    public class EstadoFactura
    {
        [Key]
        [Column("id_estado_factura")]
        public int IdEstadoFactura { get; set; }

        [Required]
        [StringLength(30)]
        [Column("estado")]
        public string Nombre { get; set; } = string.Empty;

        // Navegación
        public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }
}

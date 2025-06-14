using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionLogisticaBackend.Models
{
    [Table("metodos_pago")]
    public class MetodoPago
    {
        [Key]
        [Column("id_metodo_pago")]
        public int IdMetodoPago { get; set; }

        [Required]
        [Column("metodo_pago")]
        public string Nombre { get; set; } = string.Empty;

        // Navegación
        public virtual ICollection<MovimientoCaja> MovimientosCaja { get; set; } = new List<MovimientoCaja>();
    }
}

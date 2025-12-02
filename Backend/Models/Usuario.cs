using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GestionLogisticaBackend.Enums;
using LogisticaBackend.Models;
using Service.Enums;

namespace GestionLogisticaBackend.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [Column("email")]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        [Column("password")]
        public string Password { get; set; } = null!;

        [Required]
        public TipoRolEnum TipoRol { get; set; } = TipoRolEnum.Admin;
        public DateTime FechaRegistracion { get; set; } = DateTime.Now;
        public string Dni { get; set; } = string.Empty;
        public string Domicilio { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Observacion { get; set; } = string.Empty;
        public bool isDeleted { get; set; } = false;

        // Navegación
        public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();

    }
}

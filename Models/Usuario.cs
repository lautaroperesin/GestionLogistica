using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GestionLogisticaBackend.Enums;

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
        [Column("password_hash")]
        public string PasswordHash { get; set; } = null!;

        [Required]
        [Column("rol_usuario")]
        public string RolUsuario { get; set; } = "Admin";

        [Column("activo")]
        public bool Activo { get; set; } = true;

        [Column("ultimo_acceso")]
        public DateTime? UltimoAcceso { get; set; }

        [Required]
        [Column("fecha_alta")]
        public DateTime FechaAlta { get; set; } = DateTime.Now;

        [Column("refresh_token")]
        public string? RefreshToken { get; set; }

        [Column("fecha_expiracion_refresh_token")]
        public DateTime? FechaExpiracionRefreshToken { get; set; }

    }
}

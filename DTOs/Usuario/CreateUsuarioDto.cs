using GestionLogisticaBackend.Enums;

namespace GestionLogisticaBackend.DTOs.Usuario
{
    public class CreateUsuarioDto
    {
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Rol { get; set; } = "Admin";
        public bool Activo { get; set; } = true;
        public DateTime? UltimoAcceso { get; set; } = null;
        public DateTime FechaAlta { get; set; } = DateTime.Now;
    }
}

using GestionLogisticaBackend.Enums;
using Service.Enums;

namespace GestionLogisticaBackend.DTOs.Usuario
{
    public class CreateUsuarioDto
    {
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public TipoRolEnum Rol { get; set; }
    }
}

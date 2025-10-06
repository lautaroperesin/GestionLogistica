namespace GestionLogisticaBackend.DTOs.Usuario
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

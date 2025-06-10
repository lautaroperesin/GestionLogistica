using System.ComponentModel.DataAnnotations;

namespace GestionLogisticaBackend.DTOs.Cliente
{
    public class UpdateClienteDto
    {
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [StringLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es requerido")]
        [StringLength(100, ErrorMessage = "El teléfono no puede exceder los 100 caracteres")]
        public string Telefono { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;

namespace GestionLogisticaBackend.DTOs.Conductor
{
    public class CreateConductorDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El DNI es obligatorio")]
        public string Dni { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "La licencia es obligatoria")]
        public string ClaseLicencia { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de vencimiento de la licencia es obligatoria")]
        public DateTime VencimientoLicencia { get; set; }
    }
}

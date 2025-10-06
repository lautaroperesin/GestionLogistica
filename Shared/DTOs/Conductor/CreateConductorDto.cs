using System.ComponentModel.DataAnnotations;

namespace GestionLogisticaBackend.DTOs.Conductor
{
    public class CreateConductorDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El DNI es obligatorio")]
        [StringLength(20, ErrorMessage = "El DNI no puede exceder los 20 caracteres")]
        public string Dni { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [StringLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "La clase de licencia es requerida")]
        [StringLength(10, ErrorMessage = "La clase de licencia no puede exceder los 10 caracteres")]
        public string ClaseLicencia { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de vencimiento de la licencia es obligatoria")]
        public DateTime VencimientoLicencia { get; set; }
    }
}

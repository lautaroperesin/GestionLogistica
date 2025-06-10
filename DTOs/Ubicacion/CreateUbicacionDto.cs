using System.ComponentModel.DataAnnotations;

namespace GestionLogisticaBackend.DTOs.Ubicacion
{
    public class CreateUbicacionDto
    {
        [Required(ErrorMessage = "La dirección es requerida")]
        [StringLength(100, ErrorMessage = "La dirección no puede exceder los 100 caracteres")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La localidad es requerida")]
        public int IdLocalidad { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace GestionLogisticaBackend.DTOs.Vehiculo
{
    public class CreateVehiculoDto
    {
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La patente es obligatoria")]
        [StringLength(20, ErrorMessage = "La patente no puede exceder 20 caracteres")]
        public string Patente { get; set; } = string.Empty;
        public decimal CapacidadCarga { get; set; }
    }
}

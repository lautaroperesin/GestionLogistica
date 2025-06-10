using System.ComponentModel.DataAnnotations;

namespace GestionLogisticaBackend.DTOs.Vehiculo
{
    public class UpdateVehiculoDto
    {
        public int IdVehiculo { get; set; }

        [Required(ErrorMessage = "La marca es requerida")]
        [StringLength(50, ErrorMessage = "La marca no puede exceder los 50 caracteres")]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modelo es requerido")]
        [StringLength(50, ErrorMessage = "El modelo no puede exceder los 50 caracteres")]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La patente es requerida")]
        [StringLength(10, ErrorMessage = "La patente no puede exceder los 10 caracteres")]
        public string Patente { get; set; } = string.Empty;

        [Required(ErrorMessage = "La capacidad en kg es requerida")]
        [Range(0.01, 999999.99, ErrorMessage = "La capacidad debe ser mayor a 0")]
        public decimal CapacidadCarga { get; set; }

        [Required(ErrorMessage = "La fecha de última inspección es requerida")]
        public DateTime UltimaInspeccion { get; set; }
    }
}

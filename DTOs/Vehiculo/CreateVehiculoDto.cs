using System.ComponentModel.DataAnnotations;
using GestionLogisticaBackend.Enums;

namespace GestionLogisticaBackend.DTOs.Vehiculo
{
    public class CreateVehiculoDto
    {
        [Required(ErrorMessage = "La marca es requerida")]
        [StringLength(50, ErrorMessage = "La marca no puede exceder los 50 caracteres")]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modelo es requerido")]
        [StringLength(50, ErrorMessage = "El modelo no puede exceder los 50 caracteres")]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La patente es obligatoria")]
        [StringLength(10, ErrorMessage = "La patente no puede exceder los 10 caracteres")]
        public string Patente { get; set; } = string.Empty;

        [Required(ErrorMessage = "La capacidad en kg es requerida")]
        public decimal CapacidadCarga { get; set; }

        [Required(ErrorMessage = "El estado del vehículo es requerido")]
        public EstadoVehiculoEnum Estado { get; set; } = Enums.EstadoVehiculoEnum.Disponible;

        [Required(ErrorMessage = "La fecha de última inspección es requerida")]
        public DateTime UltimaInspeccion { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento del RTO es requerida")]
        public DateTime RtoVencimiento { get; set; }
    }
}

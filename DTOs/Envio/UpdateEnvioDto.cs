using System.ComponentModel.DataAnnotations;

namespace GestionLogisticaBackend.DTOs.Envio
{
    public class UpdateEnvioDto
    {
        public int IdEnvio { get; set; }

        [Required(ErrorMessage = "El origen es requerido")]
        public int IdOrigen { get; set; }

        [Required(ErrorMessage = "El destino es requerido")]
        public int IdDestino { get; set; }

        [Required(ErrorMessage = "El número de seguimiento es requerido")]
        [StringLength(50, ErrorMessage = "El número de seguimiento no puede exceder los 50 caracteres")]
        public string NumeroSeguimiento { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime? FechaSalidaProgramada { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FechaSalidaReal { get; set; }

        [Required(ErrorMessage = "La fecha de entrega estimada es requerida")]
        [DataType(DataType.Date)]
        public DateTime FechaEntregaEstimada { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FechaEntregaReal { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "El peso es requerido")]
        public decimal PesoKg { get; set; }

        [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El costo total es requerido")]
        public decimal CostoTotal { get; set; } = 0.00m;

        [Required(ErrorMessage = "El vehículo es requerido")]
        public int IdVehiculo { get; set; }

        [Required(ErrorMessage = "El conductor es requerido")]
        public int IdConductor { get; set; }

        [Required(ErrorMessage = "El cliente es requerido")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El tipo de carga es requerido")]
        public int IdTipoCarga { get; set; }
    }
}

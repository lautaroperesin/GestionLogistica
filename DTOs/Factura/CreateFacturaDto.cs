using System.ComponentModel.DataAnnotations;

namespace GestionLogisticaBackend.DTOs.Factura
{
    public class CreateFacturaDto
    {
        [Required(ErrorMessage = "El envío es requerido")]
        public int IdEnvio { get; set; }

        [Required(ErrorMessage = "El número de factura es requerido")]
        [StringLength(20, ErrorMessage = "El número de factura no puede exceder los 20 caracteres")]
        public string NumeroFactura { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de emisión es requerida")]
        public DateTime FechaEmision { get; set; }

        [Required(ErrorMessage = "El monto total es requerido")]
        public decimal MontoTotal { get; set; }

        [StringLength(50, ErrorMessage = "El método de pago no puede exceder los 50 caracteres")]
        public string? MetodoPago { get; set; }
    }
}

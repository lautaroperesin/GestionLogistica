using System.ComponentModel.DataAnnotations;
using GestionLogisticaBackend.Enums;
using GestionLogisticaBackend.Models;

namespace GestionLogisticaBackend.DTOs.Factura
{
    public class CreateFacturaDto
    {
        [Required(ErrorMessage = "El envío es requerido")]
        public int IdEnvio { get; set; }

        [Required(ErrorMessage = "El cliente es requerido")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El número de factura es requerido")]
        [StringLength(50, ErrorMessage = "El número de factura no puede exceder los 50 caracteres")]
        public string NumeroFactura { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de emisión es requerida")]
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }

        public EstadoFacturaEnum Estado { get; set; } = EstadoFacturaEnum.Borrador;

        [Required(ErrorMessage = "El subtotal es requerido")]
        public decimal Subtotal { get; set; }

        [Required(ErrorMessage = "El IVA es requerido")]
        public decimal Iva { get; set; }

        [Required(ErrorMessage = "El monto total es requerido")]
        public decimal Total { get; set; }
    }
}

using GestionLogisticaBackend.DTOs.Cliente;
using GestionLogisticaBackend.DTOs.Envio;
using GestionLogisticaBackend.Models;

namespace GestionLogisticaBackend.DTOs.Factura
{
    public class FacturaDto
    {
        public int IdFactura { get; set; }
        public EnvioDto Envio { get; set; } = new EnvioDto();
        public ClienteDto Cliente { get; set; } = new ClienteDto();
        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public EstadoFacturaDto EstadoFactura { get; set; } = new();
    }
}

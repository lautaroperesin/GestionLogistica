using GestionLogisticaBackend.DTOs.Envio;

namespace GestionLogisticaBackend.DTOs.Factura
{
    public class FacturaDto
    {
        public int IdFactura { get; set; }
        public int IdEnvio { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public decimal MontoTotal { get; set; }
        public string? MetodoPago { get; set; }
        public EstadoFacturaDto EstadoFactura { get; set; } = new();
    }
}

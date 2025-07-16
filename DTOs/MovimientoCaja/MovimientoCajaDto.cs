using GestionLogisticaBackend.DTOs.Factura;
using GestionLogisticaBackend.DTOs.MetodoPago;

namespace GestionLogisticaBackend.DTOs.MovimientoCaja
{
    public class MovimientoCajaDto
    {
        public int IdMovimiento { get; set; }
        public FacturaDto? Factura { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public MetodoPagoDto? MetodoPago { get; set; }
        public string? Observaciones { get; set; }
    }
}

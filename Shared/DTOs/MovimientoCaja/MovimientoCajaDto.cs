using GestionLogisticaBackend.DTOs.Factura;
using GestionLogisticaBackend.Enums;

namespace GestionLogisticaBackend.DTOs.MovimientoCaja
{
    public class MovimientoCajaDto
    {
        public int IdMovimiento { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public MetodoPagoEnum? MetodoPago { get; set; }
        public FacturaDto? Factura { get; set; }
        public string? Observaciones { get; set; }

    }
}

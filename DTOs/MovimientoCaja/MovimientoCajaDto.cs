using GestionLogisticaBackend.DTOs.Factura;

namespace GestionLogisticaBackend.DTOs.MovimientoCaja
{
    public class MovimientoCajaDto
    {
        public int IdMovimiento { get; set; }
        public FacturaDto Factura { get; set; } = new FacturaDto();
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public int IdMetodoPago { get; set; }
        public string? Observaciones { get; set; }
    }
}

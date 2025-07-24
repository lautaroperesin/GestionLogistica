using GestionLogisticaBackend.DTOs.MetodoPago;

namespace GestionLogisticaBackend.DTOs.MovimientoCaja
{
    public class MovimientoSinFacturaDto
    {
        public int IdMovimiento { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public MetodoPagoDto? MetodoPago { get; set; }
    }
}

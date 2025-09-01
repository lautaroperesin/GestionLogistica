using GestionLogisticaBackend.Enums;

namespace GestionLogisticaBackend.DTOs.MovimientoCaja
{
    public class MovimientoSinFacturaDto
    {
        public int IdMovimiento { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public MetodoPagoEnum? MetodoPago { get; set; }
    }
}

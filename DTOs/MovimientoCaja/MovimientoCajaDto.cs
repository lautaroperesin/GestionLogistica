using GestionLogisticaBackend.DTOs.Factura;
using GestionLogisticaBackend.DTOs.MetodoPago;

namespace GestionLogisticaBackend.DTOs.MovimientoCaja
{
    public class MovimientoCajaDto : MovimientoSinFacturaDto
    {
        public FacturaDto? Factura { get; set; }
        public string? Observaciones { get; set; }

    }
}

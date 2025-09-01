using GestionLogisticaBackend.DTOs.Factura;

namespace GestionLogisticaBackend.DTOs.MovimientoCaja
{
    public class MovimientoCajaDto : MovimientoSinFacturaDto
    {
        public FacturaDto? Factura { get; set; }
        public string? Observaciones { get; set; }

    }
}

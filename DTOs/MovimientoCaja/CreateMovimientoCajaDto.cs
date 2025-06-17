namespace GestionLogisticaBackend.DTOs.MovimientoCaja
{
    public class CreateMovimientoCajaDto
    {
        public int IdFactura { get; set; }
        public DateTime FechaPago { get; set; } = DateTime.Now;
        public decimal Monto { get; set; }
        public int IdMetodoPago { get; set; }
        public string? Observaciones { get; set; }
    }
}

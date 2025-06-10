namespace GestionLogisticaBackend.DTOs.Vehiculo
{
    public class VehiculoDto
    {
        public int IdVehiculo { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Patente { get; set; } = string.Empty;
        public decimal CapacidadCarga { get; set; }
    }
}

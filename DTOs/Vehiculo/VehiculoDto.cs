using GestionLogisticaBackend.Enums;

namespace GestionLogisticaBackend.DTOs.Vehiculo
{
    public class VehiculoDto
    {
        public int IdVehiculo { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Patente { get; set; } = string.Empty;
        public EstadoVehiculoEnum Estado { get; set; }
        public decimal CapacidadCarga { get; set; }
        public DateTime UltimaInspeccion { get; set; }
        public DateTime RtoVencimiento { get; set; }
    }
}

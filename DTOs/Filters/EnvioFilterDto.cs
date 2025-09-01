using GestionLogisticaBackend.DTOs.Pagination;
using GestionLogisticaBackend.Enums;

namespace GestionLogisticaBackend.DTOs.Filters
{
    public class EnvioFilterDto : PaginationParams
    {
        public int? IdConductor { get; set; }
        public int? IdCliente { get; set; }
        public int? IdVehiculo { get; set; }
        public DateTime? FechaSalidaDesde { get; set; }
        public DateTime? FechaSalidaHasta { get; set; }
        public EstadoEnvioEnum? EstadoEnvio { get; set; }
        public string? NumeroSeguimiento { get; set; }
        public string? Origen { get; set; }
        public string? Destino { get; set; }
    }
}

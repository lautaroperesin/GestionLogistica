using GestionLogisticaBackend.DTOs.Pagination;
using GestionLogisticaBackend.Enums;

namespace GestionLogisticaBackend.DTOs.Filters
{
    public class FacturaFilterDto : PaginationParams
    {
        public EstadoFacturaEnum? EstadoFactura { get; set; }
        public int? IdCliente { get; set; }
        public string? NumeroFactura { get; set; }
        public DateTime? FechaEmisionDesde { get; set; }
        public DateTime? FechaEmisionHasta { get; set; }

    }
}

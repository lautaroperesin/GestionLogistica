using GestionLogisticaBackend.DTOs.Factura;
using GestionLogisticaBackend.DTOs.Pagination;
using GestionLogisticaBackend.Enums;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IFacturaService
    {
        Task<PagedResult<FacturaDto>> GetFacturasAsync(PaginationParams pagParams);
        Task<FacturaDto?> GetFacturaByIdAsync(int id);
        Task<FacturaDto> CreateFacturaAsync(CreateFacturaDto facturaDto);
        Task<FacturaDto?> UpdateFacturaAsync(int id, UpdateFacturaDto facturaDto);
        Task<bool>DeleteFacturaAsync(int id);
        Task<List<FacturaDto>> GetFacturasPorEstadoAsync(EstadoFactura estado);
        Task<bool> ActualizarEstadoFacturaAsync(int facturaId, EstadoFactura nuevoEstado);
    }
}

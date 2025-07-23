using GestionLogisticaBackend.DTOs.Factura;
using GestionLogisticaBackend.DTOs.Filters;
using GestionLogisticaBackend.DTOs.Pagination;
using GestionLogisticaBackend.Enums;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IFacturaService
    {
        Task<PagedResult<FacturaDto>> GetFacturasAsync(FacturaFilterDto filtros);
        Task<FacturaDto?> GetFacturaByIdAsync(int id);
        Task<FacturaDto> CreateFacturaAsync(CreateFacturaDto facturaDto);
        Task<FacturaDto?> UpdateFacturaAsync(int id, UpdateFacturaDto facturaDto);
        Task<bool>DeleteFacturaAsync(int id);
        Task<bool> ActualizarEstadoFacturaAsync(int facturaId, EstadoFactura nuevoEstado);
    }
}

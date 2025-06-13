using GestionLogisticaBackend.DTOs.Factura;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IFacturaService
    {
        Task<List<FacturaDto>> GetFacturasAsync();
        Task<FacturaDto?> GetFacturaByIdAsync(int id);
        Task<FacturaDto> CreateFacturaAsync(CreateFacturaDto facturaDto);
        Task<bool>DeleteFacturaAsync(int id);
        Task<List<FacturaDto>> GetFacturasPorEstadoAsync(int estadoId);
        Task<bool> ActualizarEstadoFacturaAsync(int facturaId, int nuevoEstadoId);
    }
}

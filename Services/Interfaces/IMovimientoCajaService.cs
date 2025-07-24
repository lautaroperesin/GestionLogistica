using GestionLogisticaBackend.DTOs.MovimientoCaja;
using GestionLogisticaBackend.DTOs.Pagination;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IMovimientoCajaService
    {
        Task<PagedResult<MovimientoCajaDto>> GetMovimientosAsync(PaginationParams pagParams);
        Task<MovimientoCajaDto> GetMovimientoByIdAsync(int id);
        Task CreateMovimientoAsync(CreateMovimientoCajaDto movimientoCaja);
        Task DeleteMovimientoAsync(int id);
    }
}

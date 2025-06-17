using GestionLogisticaBackend.DTOs.MovimientoCaja;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IMovimientoCajaService
    {
        Task<IEnumerable<MovimientoCajaDto>> GetMovimientosAsync();
        Task<MovimientoCajaDto> GetMovimientoByIdAsync(int id);
        Task CreateMovimientoAsync(CreateMovimientoCajaDto movimientoCaja);
        Task UpdateMovimientoAsync(MovimientoCajaDto movimientoCaja);
        Task DeleteMovimientoAsync(int id);
    }
}

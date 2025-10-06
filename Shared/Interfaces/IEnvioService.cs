using GestionLogisticaBackend.DTOs.Envio;
using GestionLogisticaBackend.DTOs.Filters;
using GestionLogisticaBackend.DTOs.Pagination;
using GestionLogisticaBackend.Enums;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IEnvioService
    {
        Task<PagedResult<EnvioDto>> GetEnviosAsync(EnvioFilterDto filtros);
        Task<EnvioDto?> GetEnvioByIdAsync(int id);
        Task<EnvioDto> CreateEnvioAsync(CreateEnvioDto envioDto);
        Task<EnvioDto?> UpdateEnvioAsync(int id, UpdateEnvioDto envioDto);
        Task<bool> DeleteEnvioAsync(int id);
        Task UpdateEnvioEstadoAsync(int id, EstadoEnvioEnum nuevoEstado);
    }
}

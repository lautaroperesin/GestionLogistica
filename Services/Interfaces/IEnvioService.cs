using GestionLogisticaBackend.DTOs.Envio;
using GestionLogisticaBackend.DTOs.Pagination;
using LogisticaBackend.Models;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IEnvioService
    {
        Task<PagedResult<EnvioDto>> GetEnviosAsync(PaginationParams pagParams);
        Task<EnvioDto?> GetEnvioByIdAsync(int id);
        Task<EnvioDto> CreateEnvioAsync(CreateEnvioDto envioDto);
        Task<EnvioDto?> UpdateEnvioAsync(int id, UpdateEnvioDto envioDto);
        Task<bool> DeleteEnvioAsync(int id);
    }
}

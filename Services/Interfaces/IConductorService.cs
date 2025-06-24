using GestionLogisticaBackend.DTOs.Conductor;
using GestionLogisticaBackend.DTOs.Pagination;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IConductorService
    {
        Task<PagedResult<ConductorDto>> GetConductoresAsync(PaginationParams pagParams);
        Task<ConductorDto?> GetConductorByIdAsync(int id);
        Task<ConductorDto> CreateConductorAsync(CreateConductorDto conductorDto);
        Task<bool> UpdateConductorAsync(UpdateConductorDto conductorDto);
        Task<bool> DeleteConductorAsync(int id);
        Task<IEnumerable<ConductorDto>> GetConductoresConLicenciaVencidaAsync();
    }
}

using GestionLogisticaBackend.DTOs.Pagination;
using GestionLogisticaBackend.DTOs.Ubicacion;
using LogisticaBackend.Models;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IUbicacionService
    {
        Task<PagedResult<UbicacionDto>> GetUbicacionesAsync(PaginationParams pagParams);
        Task<UbicacionDto?> GetUbicacionByIdAsync(int id);
        Task<UbicacionDto> CreateUbicacionAsync(CreateUbicacionDto ubicacionDto);
        Task<bool> UpdateUbicacionAsync(UpdateUbicacionDto ubicacionDto);
        Task<bool> DeleteUbicacionAsync(int id);
    }
}

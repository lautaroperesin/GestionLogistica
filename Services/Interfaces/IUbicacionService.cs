using GestionLogisticaBackend.DTOs.Ubicacion;
using LogisticaBackend.Models;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IUbicacionService
    {
        Task<List<UbicacionDto>> GetUbicacionesAsync();
        Task<UbicacionDto?> GetUbicacionByIdAsync(int id);
        Task<UbicacionDto> CreateUbicacionAsync(CreateUbicacionDto clienteDto);
        Task<bool> UpdateUbicacionAsync(UpdateUbicacionDto clienteDto);
        Task<bool> DeleteUbicacionAsync(int id);
    }
}

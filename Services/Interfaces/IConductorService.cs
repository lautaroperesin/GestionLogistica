using GestionLogisticaBackend.DTOs.Conductor;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IConductorService
    {
        Task<List<ConductorDto>> GetConductoresAsync();
        Task<ConductorDto?> GetConductorByIdAsync(int id);
        Task<ConductorDto> CreateConductorAsync(CreateConductorDto conductorDto);
        Task<bool> UpdateConductorAsync(UpdateConductorDto conductorDto);
        Task<bool> DeleteConductorAsync(int id);
        Task<List<ConductorDto>> GetConductoresConLicenciaVencidaAsync();
    }
}

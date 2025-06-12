using GestionLogisticaBackend.DTOs.Envio;
using LogisticaBackend.Models;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IEnvioService
    {
        Task<List<EnvioDto>> GetAllEnviosAsync();
        Task<EnvioDto?> GetEnvioByIdAsync(int id);
        Task<EnvioDto> CreateEnvioAsync(CreateEnvioDto envioDto);
        Task<EnvioDto?> UpdateEnvioAsync(int id, UpdateEnvioDto envioDto);
        Task<bool> DeleteEnvioAsync(int id);
    }
}

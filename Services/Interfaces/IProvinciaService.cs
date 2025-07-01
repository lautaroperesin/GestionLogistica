using GestionLogisticaBackend.DTOs.Ubicacion;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IProvinciaService
    {
        Task<IEnumerable<ProvinciaDto>> GetProvinciasByPaisAsync(int paisId);
    }
}

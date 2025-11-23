using GestionLogisticaBackend.DTOs.Ubicacion;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IPaisService
    {
        Task<List<PaisDto>> GetPaisesAsync();
    }
}

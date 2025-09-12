using GestionLogisticaBackend.DTOs.Ubicacion;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IPaisService
    {
        Task<IEnumerable<PaisDto>> GetPaisesAsync();
    }
}

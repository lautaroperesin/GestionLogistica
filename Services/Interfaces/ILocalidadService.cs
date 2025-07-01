using GestionLogisticaBackend.DTOs.Ubicacion;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface ILocalidadService
    {
        Task<IEnumerable<LocalidadDto>> GetLocalidadesByProvinciaAsync(int provinciaId);
    }
}

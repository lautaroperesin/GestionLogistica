using GestionLogisticaBackend.DTOs.Vehiculo;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IVehiculoService
    {
        Task<List<VehiculoDto>> GetVehiculosAsync();
        Task<VehiculoDto?> GetVehiculoByIdAsync(int id);
        Task<VehiculoDto> CreateVehiculoAsync(CreateVehiculoDto vehiculoDto);
        Task<bool> UpdateVehiculoAsync(UpdateVehiculoDto vehiculoDto);
        Task<bool> DeleteVehiculoAsync(int id);
    }
}

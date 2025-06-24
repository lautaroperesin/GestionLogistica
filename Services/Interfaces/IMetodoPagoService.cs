using GestionLogisticaBackend.DTOs.MetodoPago;
using GestionLogisticaBackend.Models;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IMetodoPagoService
    {
        Task<List<MetodoPagoDto>> GetMetodosPagoAsync();
        Task<MetodoPagoDto> GetMetodoPagoByIdAsync(int id);
        Task<MetodoPagoDto> CreateMetodoPagoAsync(MetodoPagoDto metodoPago);
        Task UpdateMetodoPagoAsync(int id, MetodoPagoDto metodoPago);
        Task DeleteMetodoPagoAsync(int id);
    }
}

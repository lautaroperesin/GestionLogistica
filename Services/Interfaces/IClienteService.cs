using GestionLogisticaBackend.DTOs.Cliente;
using LogisticaBackend.Models;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IClienteService
    {
        Task<List<ClienteDto>> GetClientesAsync();
        Task<ClienteDto?> GetClienteByIdAsync(int id);
        Task<ClienteDto> CreateClienteAsync(CreateClienteDto clienteDto);
        Task<bool> UpdateClienteAsync(UpdateClienteDto clienteDto);
        Task<bool> DeleteClienteAsync(int id);
    }
}

using GestionLogisticaBackend.DTOs.Cliente;
using GestionLogisticaBackend.DTOs.Pagination;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IClienteService
    {
        Task<PagedResult<ClienteDto>> GetClientesAsync(PaginationParams pagParams);
        Task<ClienteDto?> GetClienteByIdAsync(int id);
        Task<ClienteDto> CreateClienteAsync(CreateClienteDto clienteDto);
        Task<bool> UpdateClienteAsync(UpdateClienteDto clienteDto);
        Task<bool> DeleteClienteAsync(int id);
        Task<bool> RestoreClienteAsync(int id);
        Task<IEnumerable<ClienteDto>> GetClientesEliminadosAsync();
    }
}

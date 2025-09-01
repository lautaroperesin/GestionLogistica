using GestionLogisticaBackend.DTOs.Cliente;
using GestionLogisticaBackend.DTOs.Pagination;
using GestionLogisticaBackend.Extensions;
using GestionLogisticaBackend.Services.Interfaces;
using Humanizer;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLogisticaBackend.Services.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly LogisticaContext _context;

        public ClienteService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ClienteDto>> GetClientesAsync(PaginationParams pagParams)
        {
            var query = _context.Clientes.OrderBy(c => c.Nombre);

            var totalItems = await query.CountAsync();

            var clientes = await query
                .Skip((pagParams.PageNumber - 1) * pagParams.PageSize)
                .Take(pagParams.PageSize)
                .ToListAsync();

            var clientesDto = clientes.ToDtoList();

            return new PagedResult<ClienteDto>
            {
                Items = clientesDto,
                TotalItems = totalItems,
                PageNumber = pagParams.PageNumber,
                PageSize = pagParams.PageSize
            };
        }

        public async Task<ClienteDto?> GetClienteByIdAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null) return null;

            return cliente.ToDto();
        }

        public async Task<ClienteDto> CreateClienteAsync(CreateClienteDto clienteDto)
        {
            if (clienteDto == null)
            {
                throw new ArgumentNullException(nameof(clienteDto), "El cliente no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(clienteDto.Nombre) || string.IsNullOrWhiteSpace(clienteDto.Telefono) || string.IsNullOrWhiteSpace(clienteDto.Email))
            {
                throw new ArgumentException("El cliente debe tener un nombre, teléfono y email válidos.");
            }

           var cliente = clienteDto.ToEntity();

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return cliente.ToDto();
        }

        public async Task<bool> UpdateClienteAsync(UpdateClienteDto clienteDto)
        {
            var cliente = await _context.Clientes.FindAsync(clienteDto.IdCliente);

            if (cliente == null) return false;

            cliente.UpdateFromDto(clienteDto);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null) return false;

            cliente.Deleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        // obtener clientes eliminados
        public async Task<IEnumerable<ClienteDto>> GetClientesEliminadosAsync()
        {
            var clientesEliminados = await _context.Clientes
                .IgnoreQueryFilters()
                .Where(c => c.Deleted)
                .ToListAsync();
            return clientesEliminados.ToDtoList();
        }

        // restaurar cliente
        public async Task<bool> RestoreClienteAsync(int id)
        {
            var cliente = await _context.Clientes.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.IdCliente.Equals(id));
            if (cliente == null) return false;
            cliente.Deleted = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

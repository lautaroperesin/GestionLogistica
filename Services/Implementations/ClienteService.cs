using GestionLogisticaBackend.DTOs.Cliente;
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

        public async Task<List<ClienteDto>> GetClientesAsync()
        {
            return await _context.Clientes
                .Select(c => new ClienteDto
                {
                    IdCliente = c.IdCliente,
                    Nombre = c.Nombre,
                    Telefono = c.Telefono,
                    Email = c.Email
                }).ToListAsync();
        }

        public async Task<ClienteDto?> GetClienteByIdAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null) return null;

            return new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                Telefono = cliente.Telefono,
                Email = cliente.Email
            };
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

            var cliente = new Cliente
            {
                Nombre = clienteDto.Nombre,
                Telefono = clienteDto.Telefono,
                Email = clienteDto.Email
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                Email = cliente.Email
            };
        }

        public async Task<bool> UpdateClienteAsync(UpdateClienteDto clienteDto)
        {
            var cliente = await _context.Clientes.FindAsync(clienteDto.IdCliente);

            if (cliente == null) return false;

            cliente.Nombre = clienteDto.Nombre;
            cliente.Telefono = clienteDto.Telefono;
            cliente.Email = clienteDto.Email;

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
    }
}

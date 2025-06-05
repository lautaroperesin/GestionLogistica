using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLogisticaBackend.Services
{
    public class ClienteService
    {
        private readonly LogisticaContext _context;

        public ClienteService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetClientesAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetClienteByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente> CreateClienteAsync(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(cliente.Nombre) || string.IsNullOrWhiteSpace(cliente.Telefono) || string.IsNullOrWhiteSpace(cliente.Email))
            {
                throw new ArgumentException("El cliente debe tener un nombre, teléfono y email válidos.");
            }

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }

        public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");
            }

            if (string.IsNullOrWhiteSpace(cliente.Nombre) || string.IsNullOrWhiteSpace(cliente.Telefono) || string.IsNullOrWhiteSpace(cliente.Email))
            {
                throw new ArgumentException("El cliente debe tener un nombre, teléfono y email válidos.");
            }
           
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return false;
            }
            cliente.Deleted = true; // Marcar como eliminado
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClienteExistsAsync(int id)
        {
            return await _context.Clientes.AnyAsync(c => c.IdCliente == id);
        }
    }
}

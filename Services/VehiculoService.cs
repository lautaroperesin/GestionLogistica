using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLogisticaBackend.Services
{
    public class VehiculoService
    {
        private readonly LogisticaContext _context;

        public VehiculoService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<List<Vehiculo>> GetVehiculosAsync()
        {
            return await _context.Vehiculos.ToListAsync();
        }

        public async Task<Vehiculo?> GetVehiculoByIdAsync(int id)
        {
            return await _context.Vehiculos.FindAsync(id);
        }

        public async Task<Vehiculo> CreateVehiculoAsync(Vehiculo vehiculo)
        {
            if (vehiculo == null)
            {
                throw new ArgumentNullException(nameof(vehiculo), "El vehículo no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(vehiculo.Marca) || string.IsNullOrWhiteSpace(vehiculo.Modelo) || string.IsNullOrWhiteSpace(vehiculo.Patente))
            {
                throw new ArgumentException("El vehículo debe tener una marca, modelo y patente válidos.");
            }
            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();
            return vehiculo;
        }

        public async Task<Vehiculo> UpdateVehiculoAsync(Vehiculo vehiculo)
        {
            if (vehiculo == null)
            {
                throw new ArgumentNullException(nameof(vehiculo), "El vehículo no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(vehiculo.Marca) || string.IsNullOrWhiteSpace(vehiculo.Modelo) || string.IsNullOrWhiteSpace(vehiculo.Patente))
            {
                throw new ArgumentException("El vehículo debe tener una marca, modelo y placa válidos.");
            }
            _context.Entry(vehiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return vehiculo;
        }

        public async Task<bool> DeleteVehiculoAsync(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
            {
                return false; // Vehículo no encontrado
            }

            // Marcar como eliminado en lugar de eliminar físicamente
            vehiculo.Deleted = true;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

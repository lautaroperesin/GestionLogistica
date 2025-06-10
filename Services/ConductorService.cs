using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLogisticaBackend.Services
{
    public class ConductorService
    {
        private readonly LogisticaContext _context;

        public ConductorService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<List<Conductor>> GetConductoresAsync()
        {
            return await _context.Conductores.ToListAsync();
        }

        public async Task<Conductor?> GetConductorByIdAsync(int id)
        {
            return await _context.Conductores.FindAsync(id);
        }

        public async Task<Conductor> CreateConductorAsync(Conductor conductor)
        {
            if (conductor == null)
            {
                throw new ArgumentNullException(nameof(conductor), "El conductor no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(conductor.Nombre) || string.IsNullOrWhiteSpace(conductor.Telefono) || string.IsNullOrWhiteSpace(conductor.Email))
            {
                throw new ArgumentException("El conductor debe tener un nombre, teléfono y email válidos.");
            }
            _context.Conductores.Add(conductor);
            await _context.SaveChangesAsync();
            return conductor;
        }

        public async Task<Conductor> UpdateConductorAsync(Conductor conductor)
        {
            if (conductor == null)
            {
                throw new ArgumentNullException(nameof(conductor), "El conductor no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(conductor.Nombre) || string.IsNullOrWhiteSpace(conductor.Telefono) || string.IsNullOrWhiteSpace(conductor.Email))
            {
                throw new ArgumentException("El conductor debe tener un nombre, teléfono y email válidos.");
            }
            _context.Entry(conductor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return conductor;
        }

        public async Task<bool> DeleteConductorAsync(int id)
        {
            var conductor = await _context.Conductores.FindAsync(id);
            if (conductor == null)
            {
                return false; // No se encontró el conductor
            }

            // soft delete: marcar como eliminado


            await _context.SaveChangesAsync();
            return true; // Conductor eliminado exitosamente
        }
    }
}

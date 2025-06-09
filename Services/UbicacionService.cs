using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace GestionLogisticaBackend.Services
{
    public class UbicacionService
    {
        private readonly LogisticaContext _context;

        public UbicacionService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<List<Ubicacion>> GetUbicacionesAsync()
        {
            return await _context.Ubicaciones.Include(u => u.Localidad).ToListAsync();
        }

        public async Task<Ubicacion> GetUbicacionByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID de la ubicación debe ser mayor que cero.", nameof(id));
            }
            if (await _context.Ubicaciones.AnyAsync(u => u.IdUbicacion == id) == false)
            {
                throw new KeyNotFoundException($"No se encontró una ubicación con el ID {id}.");
            }
            
            return await _context.Ubicaciones
                .Include(u => u.Localidad)
                .FirstOrDefaultAsync(u => u.IdUbicacion == id);
        }

        public async Task<Ubicacion> CreateUbicacionAsync(Ubicacion ubicacion)
        {
            if (ubicacion == null)
            {
                throw new ArgumentNullException(nameof(ubicacion), "La ubicación no puede ser nula.");
            }
            if (string.IsNullOrWhiteSpace(ubicacion.Direccion) || ubicacion.IdLocalidad <= 0)
            {
                throw new ArgumentException("La ubicación debe tener una dirección válida y un ID de localidad válido.");
            }

            _context.Ubicaciones.Add(ubicacion);
            await _context.SaveChangesAsync();
            return ubicacion;
        }

        public async Task<Ubicacion> UpdateUbicacionAsync(Ubicacion ubicacion)
        {
            if (ubicacion == null)
            {
                throw new ArgumentNullException(nameof(ubicacion), "La ubicación no puede ser nula.");
            }
            if (string.IsNullOrWhiteSpace(ubicacion.Direccion) || ubicacion.IdLocalidad <= 0)
            {
                throw new ArgumentException("La ubicación debe tener una dirección válida y un ID de localidad válido.");
            }

            _context.Entry(ubicacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ubicacion;
        }

        public async Task<bool> DeleteUbicacionAsync(int id)
        {
            var ubicacion = await GetUbicacionByIdAsync(id);
            if (ubicacion == null)
            {
                return false;
            }

            // Marcar como eliminada en lugar de eliminar físicamente
            ubicacion.Deleted = true;
            _context.Entry(ubicacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

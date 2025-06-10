using GestionLogisticaBackend.DTOs.Ubicacion;
using GestionLogisticaBackend.Services.Interfaces;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace GestionLogisticaBackend.Services.Implementations
{
    public class UbicacionService : IUbicacionService
    {
        private readonly LogisticaContext _context;

        public UbicacionService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<List<UbicacionDto>> GetUbicacionesAsync()
        {
            var ubicaciones = await _context.Ubicaciones
                .Include(u => u.Localidad)
                .ToListAsync();

            return ubicaciones.Select(u => new UbicacionDto
            {
                IdUbicacion = u.IdUbicacion,
                Direccion = u.Direccion,
                IdLocalidad = u.IdLocalidad,
                LocalidadNombre = u.Localidad.Nombre
            }).ToList();
        }

        public async Task<UbicacionDto?> GetUbicacionByIdAsync(int id)
        {

            var ubicacion = await _context.Ubicaciones
                .Include(u => u.Localidad)
                .FirstOrDefaultAsync(u => u.IdUbicacion == id);

            if (ubicacion == null) return null;

            return new UbicacionDto
            {
                IdUbicacion = ubicacion.IdUbicacion,
                Direccion = ubicacion.Direccion,
                IdLocalidad = ubicacion.IdLocalidad,
                LocalidadNombre = ubicacion.Localidad.Nombre
            };
        }

        public async Task<UbicacionDto> CreateUbicacionAsync(CreateUbicacionDto ubicacionDto)
        {
            if (ubicacionDto == null)
            {
                throw new ArgumentNullException(nameof(ubicacionDto), "La ubicación no puede ser nula.");
            }
            if (string.IsNullOrWhiteSpace(ubicacionDto.Direccion) || ubicacionDto.IdLocalidad <= 0)
            {
                throw new ArgumentException("La ubicación debe tener una dirección válida y un ID de localidad válido.");
            }

            var ubicacion = new Ubicacion
            {
                Direccion = ubicacionDto.Direccion,
                IdLocalidad = ubicacionDto.IdLocalidad
            };

            _context.Ubicaciones.Add(ubicacion);
            await _context.SaveChangesAsync();

            // Cargar la localidad asociada
            //await _context.Entry(ubicacion).Reference(u => u.Localidad).LoadAsync();

            return new UbicacionDto
            {
                IdUbicacion = ubicacion.IdUbicacion,
                Direccion = ubicacion.Direccion,
                IdLocalidad = ubicacion.IdLocalidad,
                //LocalidadNombre = ubicacion.Localidad.Nombre
            };
        }

        public async Task<bool> UpdateUbicacionAsync(UpdateUbicacionDto ubicacionDto)
        {
            if (ubicacionDto == null)
            {
                throw new ArgumentNullException(nameof(ubicacionDto), "La ubicación no puede ser nula.");
            }
            if (string.IsNullOrWhiteSpace(ubicacionDto.Direccion) || ubicacionDto.IdLocalidad <= 0)
            {
                throw new ArgumentException("La ubicación debe tener una dirección válida y un ID de localidad válido.");
            }

            var ubicacion = await _context.Ubicaciones.FindAsync(ubicacionDto.IdUbicacion);

            if (ubicacion == null) return false;

            ubicacion.Direccion = ubicacionDto.Direccion;
            ubicacion.IdLocalidad = ubicacionDto.IdLocalidad;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUbicacionAsync(int id)
        {
            var ubicacion = await _context.Ubicaciones.FindAsync(id);
            if (ubicacion == null) return false;

            ubicacion.Deleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

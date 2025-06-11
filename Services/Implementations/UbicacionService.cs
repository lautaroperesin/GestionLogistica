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
                 .ThenInclude(l => l.Provincia)
                 .ThenInclude(p => p.Pais)
                .ToListAsync();

            return ubicaciones.Select(u => new UbicacionDto
            {
                IdUbicacion = u.IdUbicacion,
                Direccion = u.Direccion,
                Localidad = new LocalidadDto
                {
                    IdLocalidad = u.Localidad.IdLocalidad,
                    Nombre = u.Localidad.Nombre,
                    Provincia = new ProvinciaDto
                    {
                        IdProvincia = u.Localidad.Provincia.IdProvincia,
                        Nombre = u.Localidad.Provincia.Nombre,
                        Pais = new PaisDto
                        {
                            IdPais = u.Localidad.Provincia.Pais.IdPais,
                            Nombre = u.Localidad.Provincia.Pais.Nombre,
                            CodigoIso = u.Localidad.Provincia.Pais.CodigoIso
                        }
                    }
                },
            }).ToList();
        }

        public async Task<UbicacionDto?> GetUbicacionByIdAsync(int id)
        {
            var ubicacion = await _context.Ubicaciones
                .Include(u => u.Localidad)
                .ThenInclude(l => l.Provincia)
                .ThenInclude(p => p.Pais)
                .FirstOrDefaultAsync(u => u.IdUbicacion == id);

            if (ubicacion == null) return null;

            return new UbicacionDto
            {
                IdUbicacion = ubicacion.IdUbicacion,
                Direccion = ubicacion.Direccion,
                Localidad = new LocalidadDto
                {
                    IdLocalidad = ubicacion.Localidad.IdLocalidad,
                    Nombre = ubicacion.Localidad.Nombre,
                    Provincia = new ProvinciaDto
                    {
                        IdProvincia = ubicacion.Localidad.Provincia.IdProvincia,
                        Nombre = ubicacion.Localidad.Provincia.Nombre,
                        Pais = new PaisDto
                        {
                            IdPais = ubicacion.Localidad.Provincia.Pais.IdPais,
                            Nombre = ubicacion.Localidad.Provincia.Pais.Nombre,
                            CodigoIso = ubicacion.Localidad.Provincia.Pais.CodigoIso
                        }
                    }
                }
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
                Localidad = new LocalidadDto
                {
                    IdLocalidad = ubicacion.IdLocalidad,
                    Nombre = ubicacion.Localidad?.Nombre ?? string.Empty,
                    Provincia = new ProvinciaDto
                    {
                        IdProvincia = ubicacion.Localidad?.Provincia?.IdProvincia ?? 0,
                        Nombre = ubicacion.Localidad?.Provincia?.Nombre ?? string.Empty
                    }
                }
            };
        }

        public async Task<bool> UpdateUbicacionAsync(UpdateUbicacionDto ubicacionDto)
        {
            var ubicacion = await _context.Ubicaciones.FindAsync(ubicacionDto.IdUbicacion);

            if (ubicacion == null) return false;

            ubicacion.Direccion = ubicacionDto.Direccion;

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

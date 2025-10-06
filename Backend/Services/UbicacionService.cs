using GestionLogisticaBackend.DTOs.Pagination;
using GestionLogisticaBackend.DTOs.Ubicacion;
using GestionLogisticaBackend.Extensions;
using GestionLogisticaBackend.Services.Interfaces;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace GestionLogisticaBackend.Implementations
{
    public class UbicacionService : IUbicacionService
    {
        private readonly LogisticaContext _context;

        public UbicacionService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<UbicacionDto>> GetUbicacionesAsync(PaginationParams pagParams)
        {
            var query = _context.Ubicaciones
                .Include(u => u.Localidad)
                    .ThenInclude(l => l.Provincia)
                    .ThenInclude(p => p.Pais);

            var totalItems = await query.CountAsync();

            var ubicaciones = await query
                .Skip((pagParams.PageNumber - 1) * pagParams.PageSize)
                .Take(pagParams.PageSize)
                .ToListAsync();

            var ubicacionesDto = ubicaciones.ToDtoList();

            return new PagedResult<UbicacionDto>
            {
                Items = ubicacionesDto,
                TotalItems = totalItems,
                PageNumber = pagParams.PageNumber,
                PageSize = pagParams.PageSize
            };
        }

        public async Task<UbicacionDto?> GetUbicacionByIdAsync(int id)
        {
            var ubicacion = await _context.Ubicaciones
                .Include(u => u.Localidad)
                .ThenInclude(l => l.Provincia)
                .ThenInclude(p => p.Pais)
                .FirstOrDefaultAsync(u => u.IdUbicacion == id);

            if (ubicacion == null) return null;

            return ubicacion.ToDto();
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

            var ubicacion = ubicacionDto.ToEntity();

            _context.Ubicaciones.Add(ubicacion);
            await _context.SaveChangesAsync();

            // Cargar la localidad y sus relaciones para devolver el DTO completo
            var ubicacionCreada = await _context.Ubicaciones
                .Include(u => u.Localidad)
                    .ThenInclude(l => l.Provincia)
                    .ThenInclude(p => p.Pais)
                .FirstOrDefaultAsync(u => u.IdUbicacion == ubicacion.IdUbicacion);

            if (ubicacionCreada == null)
                throw new Exception("Error al obtener la ubicación luego de crearla.");

            return ubicacionCreada.ToDto();
        }

        public async Task<bool> UpdateUbicacionAsync(UpdateUbicacionDto ubicacionDto)
        {
            var ubicacion = await _context.Ubicaciones.FindAsync(ubicacionDto.IdUbicacion);

            if (ubicacion == null) return false;

            ubicacion.Direccion = ubicacionDto.Direccion;
            ubicacion.Descripcion = ubicacionDto.Descripcion;

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

        public async Task<IEnumerable<PaisDto>> GetPaisesAsync()
        {
            return await _context.Paises
                .Select(p => new PaisDto
                {
                    IdPais = p.IdPais,
                    Nombre = p.Nombre,
                    CodigoIso = p.CodigoIso,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ProvinciaDto>> GetProvinciasByPaisAsync(int paisId)
        {
            return await _context.Provincias
                .Where(p => p.IdPais == paisId)
                .Select(p => new ProvinciaDto
                {
                    IdProvincia = p.IdProvincia,
                    Nombre = p.Nombre,
                    Pais = new PaisDto { IdPais = paisId }
                })
                .OrderBy(p => p.Nombre)
                .ToListAsync();
        }

        public async Task<IEnumerable<LocalidadDto>> GetLocalidadesByProvinciaAsync(int provinciaId)
        {
            return await _context.Localidades
                .Include(l => l.Provincia)
                .Where(l => l.IdProvincia == provinciaId)
                .Select(l => new LocalidadDto
                {
                    IdLocalidad = l.IdLocalidad,
                    Nombre = l.Nombre,
                    Provincia = new ProvinciaDto { IdProvincia = provinciaId }
                })
                .OrderBy(l => l.Nombre)
                .ToListAsync();
        }
    }
}

using System.Diagnostics;
using GestionLogisticaBackend.DTOs.Cliente;
using GestionLogisticaBackend.DTOs.Conductor;
using GestionLogisticaBackend.DTOs.Envio;
using GestionLogisticaBackend.DTOs.Filters;
using GestionLogisticaBackend.DTOs.Pagination;
using GestionLogisticaBackend.DTOs.Ubicacion;
using GestionLogisticaBackend.DTOs.Vehiculo;
using GestionLogisticaBackend.Extensions;
using GestionLogisticaBackend.Services.Interfaces;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLogisticaBackend.Services.Implementations
{
    public class EnvioService : IEnvioService
    {
        private readonly LogisticaContext _context;

        public EnvioService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<EnvioDto>> GetEnviosAsync(EnvioFilterDto filtros)
        {
            var query = GetEnvioWithIncludes();

            // Aplicar filtros a la query
            query = ApplyFilters(query, filtros);

            query = query.OrderByDescending(e => e.FechaCreacionEnvio);

            var totalItems = await query.CountAsync();

            var envios = await query
                .Skip((filtros.PageNumber - 1) * filtros.PageSize)
                .Take(filtros.PageSize)
                .ToListAsync();

            var enviosDto = envios.ToDtoList();

            return new PagedResult<EnvioDto>
            {
                Items = enviosDto,
                TotalItems = totalItems,
                PageNumber = filtros.PageNumber,
                PageSize = filtros.PageSize
            };
        }

        public async Task<EnvioDto?> GetEnvioByIdAsync(int id)
        {
            var envio = await GetEnvioWithIncludes().FirstOrDefaultAsync(e => e.IdEnvio == id);
            
            if (envio == null) return null;

            return envio.ToDto();
        }

        public async Task<EnvioDto> CreateEnvioAsync(CreateEnvioDto envioDto)
        {
            var envio = envioDto.ToEntity();

            _context.Envios.Add(envio);

            await _context.SaveChangesAsync();

            var envioCreado = await GetEnvioWithIncludes().FirstOrDefaultAsync(e => e.IdEnvio == envio.IdEnvio);

            if (envioCreado == null)
                throw new Exception("Error al obtener el envío luego de crearlo.");

            return envioCreado.ToDto();
        }

        public async Task<EnvioDto?> UpdateEnvioAsync(int id, UpdateEnvioDto envioDto)
        {
            var envio = await _context.Envios.FindAsync(id);

            if (envio == null)
            {
                return null;
            }

            envio.UpdateFromDto(envioDto);

            await _context.SaveChangesAsync();

            var envioActualizado = await GetEnvioWithIncludes().FirstOrDefaultAsync(e => e.IdEnvio == envio.IdEnvio);

            if (envioActualizado == null)
                throw new Exception("Error al obtener el envío luego de actualizarlo.");

            return envioActualizado.ToDto();
        }

        public async Task<bool> DeleteEnvioAsync(int id)
        {
            var envio = await _context.Envios.FindAsync(id);
            if (envio == null) return false;

            envio.Deleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        // actualizar estado del envio - seria un patch en el controller
        public async Task UpdateEnvioEstadoAsync(int id, int nuevoEstadoId)
        {
            var envio = await _context.Envios.FindAsync(id);
            if (envio == null)
            {
                throw new KeyNotFoundException($"No se encontró el envío con ID {id}.");
            }
            envio.IdEstado = nuevoEstadoId;
            await _context.SaveChangesAsync();
        }

        private IQueryable<Envio> GetEnvioWithIncludes()
        {
            return _context.Envios
                .Include(e => e.Origen)
                    .ThenInclude(o => o.Localidad)
                        .ThenInclude(l => l.Provincia)
                            .ThenInclude(p => p.Pais)
                .Include(e => e.Destino)
                    .ThenInclude(d => d.Localidad)
                        .ThenInclude(l => l.Provincia)
                            .ThenInclude(p => p.Pais)
                .Include(e => e.Estado)
                .Include(e => e.Vehiculo)
                .Include(e => e.Conductor)
                .Include(e => e.Cliente)
                .Include(e => e.TipoCarga);
        }

        // aplicar filtros a la query
        private IQueryable<Envio> ApplyFilters(IQueryable<Envio> query, EnvioFilterDto filtros)
        {
            if (!string.IsNullOrEmpty(filtros.NumeroSeguimiento))
            {
                query = query.Where(e => e.NumeroSeguimiento.Contains(filtros.NumeroSeguimiento));
            }
            if (filtros.FechaSalidaDesde.HasValue)
            {
                query = query.Where(e => e.FechaCreacionEnvio >= filtros.FechaSalidaDesde.Value);
            }
            if (filtros.FechaSalidaHasta.HasValue)
            {
                query = query.Where(e => e.FechaCreacionEnvio <= filtros.FechaSalidaHasta.Value);
            }
            if (filtros.EstadoEnvio.HasValue)
            {
                query = query.Where(e => e.IdEstado == filtros.EstadoEnvio.Value);
            }
            if (filtros.IdConductor.HasValue)
            {
                query = query.Where(e => e.IdConductor == filtros.IdConductor.Value);
            }
            if (filtros.IdCliente.HasValue)
            {
                query = query.Where(e => e.IdCliente == filtros.IdCliente.Value);
            }
            if (filtros.IdVehiculo.HasValue)
            {
                query = query.Where(e => e.IdVehiculo == filtros.IdVehiculo.Value);
            }
            if (!string.IsNullOrEmpty(filtros.Origen))
            {
                query = query.Where(e => e.Origen.Localidad.Nombre.Contains(filtros.Origen));
            }
            if (!string.IsNullOrEmpty(filtros.Destino))
            {
                query = query.Where(e => e.Destino.Localidad.Nombre.Contains(filtros.Destino));
            }

            return query;
        }
    }
}

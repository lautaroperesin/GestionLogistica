using GestionLogisticaBackend.DTOs.Cliente;
using GestionLogisticaBackend.DTOs.Envio;
using GestionLogisticaBackend.DTOs.Factura;
using GestionLogisticaBackend.DTOs.Filters;
using GestionLogisticaBackend.DTOs.Pagination;
using GestionLogisticaBackend.DTOs.Ubicacion;
using GestionLogisticaBackend.Enums;
using GestionLogisticaBackend.Extensions;
using GestionLogisticaBackend.Services.Interfaces;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLogisticaBackend.Implementations
{
    public class FacturaService : IFacturaService
    {
        private readonly LogisticaContext _context;

        public FacturaService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<FacturaDto>> GetFacturasAsync(FacturaFilterDto filtros)
        {
            var query = GetFacturaWithIncludes();

            query = ApplyFilters(query, filtros);

            query = query.OrderByDescending(f => f.FechaEmision);

            var totalItems = await query.CountAsync();

            var facturas = await query
                .Skip((filtros.PageNumber - 1) * filtros.PageSize)
                .Take(filtros.PageSize)
                .ToListAsync();

            var facturasDto = facturas.ToDtoList();

            return new PagedResult<FacturaDto>
            {
                Items = facturasDto,
                TotalItems = totalItems,
                PageNumber = filtros.PageNumber,
                PageSize = filtros.PageSize
            };
        }

        public async Task<FacturaDto?> GetFacturaByIdAsync(int id)
        {
            var factura = await GetFacturaWithIncludes().FirstOrDefaultAsync(f => f.IdFactura == id);

            if (factura == null) return null;

            return factura.ToDto();
        }

        public async Task<FacturaDto> CreateFacturaAsync(CreateFacturaDto facturaDto)
        {
            var factura = facturaDto.ToEntity();

            _context.Facturas.Add(factura);

            await _context.SaveChangesAsync();

            var facturaCreada = await GetFacturaWithIncludes().FirstOrDefaultAsync(f => f.IdFactura == factura.IdFactura);

            if (facturaCreada == null) throw new Exception("Error al crear la factura");

            return facturaCreada.ToDto();
        }

        // update factura
        public async Task<FacturaDto?> UpdateFacturaAsync(int id, UpdateFacturaDto facturaDto)
        {
            var factura = await _context.Facturas.FindAsync(id);

            if (factura == null) return null;

            factura.UpdateFromDto(facturaDto);

            await _context.SaveChangesAsync();

            var facturaActualizada = await GetFacturaWithIncludes().FirstOrDefaultAsync(f => f.IdFactura == factura.IdFactura);

            if (facturaActualizada == null) throw new Exception("Error al actualizar la factura");

            return facturaActualizada.ToDto();
        }


        public async Task<bool> DeleteFacturaAsync(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);

            if (factura == null) return false;

            factura.Deleted = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarEstadoFacturaAsync(int facturaId, EstadoFacturaEnum nuevoEstado)
        {
            var factura = await _context.Facturas.FindAsync(facturaId);

            if (factura == null) return false;

            factura.Estado = nuevoEstado;

            await _context.SaveChangesAsync();
            return true;
        }

        private IQueryable<Factura> GetFacturaWithIncludes()
        {
            return _context.Facturas
                .Include(f => f.MovimientosCaja)
                .Include(f => f.Envio)
                    .ThenInclude(e => e.Origen)
                        .ThenInclude(o => o.Localidad)
                            .ThenInclude(l => l.Provincia)
                                .ThenInclude(p => p.Pais)
                .Include(f => f.Envio)
                    .ThenInclude(e => e.Destino)
                        .ThenInclude(d => d.Localidad)
                            .ThenInclude(l => l.Provincia)
                                .ThenInclude(p => p.Pais)
                .Include(f => f.Envio.TipoCarga)
                .Include(f => f.Cliente);
        }

        private IQueryable<Factura> ApplyFilters(IQueryable<Factura> query, FacturaFilterDto filtros)
        {
            if (!string.IsNullOrEmpty(filtros.NumeroFactura))
            {
                query = query.Where(f => f.NumeroFactura.Contains(filtros.NumeroFactura));
            }
            if (filtros.EstadoFactura.HasValue)
            {
                query = query.Where(f => f.Estado == filtros.EstadoFactura.Value);
            }
            if (filtros.FechaEmisionDesde.HasValue)
            {
                query = query.Where(f => f.FechaEmision >= filtros.FechaEmisionDesde.Value);
            }
            if (filtros.FechaEmisionHasta.HasValue)
            {
                query = query.Where(f => f.FechaEmision <= filtros.FechaEmisionHasta.Value);
            }
            if (filtros.IdCliente.HasValue)
            {
                query = query.Where(f => f.Cliente.IdCliente == filtros.IdCliente.Value);
            }
            return query;
        }
    }
}

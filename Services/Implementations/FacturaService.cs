using GestionLogisticaBackend.DTOs.Cliente;
using GestionLogisticaBackend.DTOs.Envio;
using GestionLogisticaBackend.DTOs.Factura;
using GestionLogisticaBackend.DTOs.Pagination;
using GestionLogisticaBackend.DTOs.Ubicacion;
using GestionLogisticaBackend.Enums;
using GestionLogisticaBackend.Extensions;
using GestionLogisticaBackend.Services.Interfaces;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLogisticaBackend.Services.Implementations
{
    public class FacturaService : IFacturaService
    {
        private readonly LogisticaContext _context;

        public FacturaService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<FacturaDto>> GetFacturasAsync(PaginationParams pagParams)
        {
            var query = GetFacturaWithIncludes();

            var totalItems = await query.CountAsync();

            var facturas = await query
                .Skip((pagParams.PageNumber - 1) * pagParams.PageSize)
                .Take(pagParams.PageSize)
                .ToListAsync();

            var facturasDto = facturas.ToDtoList();

            return new PagedResult<FacturaDto>
            {
                Items = facturasDto,
                TotalItems = totalItems,
                PageNumber = pagParams.PageNumber,
                PageSize = pagParams.PageSize
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

        public async Task<List<FacturaDto>> GetFacturasPorEstadoAsync(EstadoFactura estado)
        {
            var facturas = await GetFacturaWithIncludes().ToListAsync();

            return (List<FacturaDto>)facturas.ToDtoList();
        }

        public async Task<bool> ActualizarEstadoFacturaAsync(int facturaId, EstadoFactura nuevoEstado)
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
                .Include(f => f.Cliente)
                .OrderBy(f => f.FechaEmision);
        }
    }
}

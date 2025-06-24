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
            var query = _context.Facturas
                .Where(f => !f.Deleted)
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
            var factura = await _context.Facturas.FirstOrDefaultAsync(f => f.IdFactura == id);

            if (factura == null) return null;

            return factura.ToDto();
        }

        public async Task<FacturaDto> CreateFacturaAsync(CreateFacturaDto facturaDto)
        {
            if (facturaDto == null)
            {
                throw new ArgumentNullException(nameof(facturaDto), "La factura no puede ser nula.");
            }

            var factura = facturaDto.ToEntity();

            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return factura.ToDto();
        }

        // update factura
        public async Task<FacturaDto?> UpdateFacturaAsync(int id, UpdateFacturaDto facturaDto)
        {
            if (facturaDto == null)
            {
                throw new ArgumentNullException(nameof(facturaDto), "La factura no puede ser nula.");
            }

            var factura = await _context.Facturas.FindAsync(id);

            if (factura == null) return null;

            factura.UpdateFromDto(facturaDto);

            await _context.SaveChangesAsync();

            return factura.ToDto();
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
            var query = _context.Facturas
                .Where(f => f.Estado == estado)
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

            var facturas = await query.ToListAsync();

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
    }
}

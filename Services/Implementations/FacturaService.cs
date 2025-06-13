using GestionLogisticaBackend.DTOs.Factura;
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

        public async Task<List<FacturaDto>> GetFacturasAsync()
        {
            return await _context.Facturas
                .Select(f => new FacturaDto
                {
                    IdFactura = f.IdFactura,
                    FechaEmision = f.FechaEmision,
                    MontoTotal = f.MontoTotal,
                    EstadoFactura = new EstadoFacturaDto
                    {
                        IdEstadoFactura = f.EstadoFactura.IdEstadoFactura,
                        Nombre = f.EstadoFactura.Nombre
                    },
                    NumeroFactura = f.NumeroFactura,
                    MetodoPago = f.MetodoPago,
                    IdEnvio = f.IdEnvio
                }).ToListAsync();
        }

        public async Task<FacturaDto?> GetFacturaByIdAsync(int id)
        {
            var factura = await _context.Facturas
                .Include(f => f.EstadoFactura)
                .FirstOrDefaultAsync(f => f.IdFactura == id);

            if (factura == null) return null;

            return new FacturaDto
            {
                IdFactura = factura.IdFactura,
                FechaEmision = factura.FechaEmision,
                MontoTotal = factura.MontoTotal,
                EstadoFactura = new EstadoFacturaDto
                {
                    IdEstadoFactura = factura.EstadoFactura.IdEstadoFactura,
                    Nombre = factura.EstadoFactura.Nombre
                },
                NumeroFactura = factura.NumeroFactura,
                MetodoPago = factura.MetodoPago,
                IdEnvio = factura.IdEnvio
            };
        }

        public async Task<FacturaDto> CreateFacturaAsync(CreateFacturaDto facturaDto)
        {
            if (facturaDto == null)
            {
                throw new ArgumentNullException(nameof(facturaDto), "La factura no puede ser nula.");
            }

            var factura = new Factura
            {
                IdEnvio = facturaDto.IdEnvio,
                NumeroFactura = facturaDto.NumeroFactura,
                FechaEmision = DateTime.UtcNow,
                MontoTotal = facturaDto.MontoTotal,
                MetodoPago = facturaDto.MetodoPago,
                IdEstadoFactura = 1
            };

            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return new FacturaDto
            {
                IdFactura = factura.IdFactura,
                FechaEmision = factura.FechaEmision,
                MontoTotal = factura.MontoTotal,
                EstadoFactura = new EstadoFacturaDto
                {
                    IdEstadoFactura = factura.IdEstadoFactura      
                },
                NumeroFactura = factura.NumeroFactura,
                MetodoPago = factura.MetodoPago,
                IdEnvio = factura.IdEnvio
            };
        }

        public async Task<bool> DeleteFacturaAsync(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);

            if (factura == null) return false;

            factura.Deleted = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<FacturaDto>> GetFacturasPorEstadoAsync(int estadoId)
        {
            return await _context.Facturas
                .Where(f => f.IdEstadoFactura == estadoId)
                .Select(f => new FacturaDto
                {
                    IdFactura = f.IdFactura,
                    FechaEmision = f.FechaEmision,
                    MontoTotal = f.MontoTotal,
                    EstadoFactura = new EstadoFacturaDto
                    {
                        IdEstadoFactura = f.EstadoFactura.IdEstadoFactura,
                        Nombre = f.EstadoFactura.Nombre
                    },
                    NumeroFactura = f.NumeroFactura,
                    MetodoPago = f.MetodoPago,
                    IdEnvio = f.IdEnvio
                }).ToListAsync();
        }

        public async Task<bool> ActualizarEstadoFacturaAsync(int facturaId, int nuevoEstadoId)
        {
            var factura = await _context.Facturas.FindAsync(facturaId);

            if (factura == null) return false;

            factura.IdEstadoFactura = nuevoEstadoId;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

using System.Diagnostics;
using GestionLogisticaBackend.DTOs.Factura;
using GestionLogisticaBackend.DTOs.MovimientoCaja;
using GestionLogisticaBackend.DTOs.Pagination;
using GestionLogisticaBackend.Enums;
using GestionLogisticaBackend.Extensions;
using GestionLogisticaBackend.Models;
using GestionLogisticaBackend.Services.Interfaces;
using LogisticaBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionLogisticaBackend.Services.Implementations
{
    public class MovimientoCajaService : IMovimientoCajaService
    {
        private readonly LogisticaContext _context;
        private readonly IFacturaService _facturaService;

        public MovimientoCajaService(LogisticaContext context, IFacturaService facturaService)
        {
            _context = context;
            _facturaService = facturaService;
        }

        public async Task<PagedResult<MovimientoCajaDto>> GetMovimientosAsync(PaginationParams pagParams)
        {
            var query = GetMovimientosQuery().OrderByDescending(m => m.FechaPago);

            var totalItems = await query.CountAsync();

            var movimientos = await query
                .Skip((pagParams.PageNumber - 1) * pagParams.PageSize)
                .Take(pagParams.PageSize)
                .ToListAsync();

            var movimientosDto = movimientos.ToDtoList();

            return new PagedResult<MovimientoCajaDto>
            {
                Items = movimientosDto,
                TotalItems = totalItems,
                PageNumber = pagParams.PageNumber,
                PageSize = pagParams.PageSize
            };

        }

        public async Task<MovimientoCajaDto> GetMovimientoByIdAsync(int id)
        {
            var movimiento = await GetMovimientosQuery().FirstOrDefaultAsync(m => m.IdMovimiento == id);

            if (movimiento == null) return null;

            return movimiento.ToDto();
        }

        public async Task CreateMovimientoAsync(CreateMovimientoCajaDto movimientoCajaDto)
        {
            // Validaciones básicas
            if (movimientoCajaDto == null)
                throw new ArgumentNullException(nameof(movimientoCajaDto), "El movimiento de caja no puede ser nulo.");

            if (movimientoCajaDto.Monto <= 0)
                throw new ArgumentException("El monto debe ser mayor a cero.", nameof(movimientoCajaDto.Monto));

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Obtener factura con bloqueo para evitar condiciones de carrera
                var factura = await _context.Facturas
                    .Include(f => f.MovimientosCaja)
                    .Where(f => f.IdFactura == movimientoCajaDto.IdFactura)
                    .FirstOrDefaultAsync();

                if (factura == null)
                    throw new KeyNotFoundException($"No se encontró una factura con ID {movimientoCajaDto.IdFactura}.");

                // Verificar que la factura no esté ya completamente pagada
                if (factura.Estado == EstadoFactura.Pagada)
                    throw new InvalidOperationException("No se pueden agregar pagos a una factura ya pagada completamente.");

                // Calcular saldo pendiente actual
                decimal totalPagadoActual = factura.MovimientosCaja.Sum(m => m.Monto);
                decimal saldoPendienteActual = factura.Total - totalPagadoActual;

                // Validar que no se exceda el saldo pendiente
                if (movimientoCajaDto.Monto > saldoPendienteActual)
                {
                    throw new InvalidOperationException(
                        $"El monto a pagar ({movimientoCajaDto.Monto:C}) excede el saldo pendiente ({saldoPendienteActual:C}).");
                }

                // Crear el nuevo movimiento
                var nuevoMovimiento = movimientoCajaDto.ToEntity();

                _context.MovimientosCaja.Add(nuevoMovimiento);
                await _context.SaveChangesAsync();

                // Actualizar estado de la factura basado en los nuevos totales
                await ActualizarEstadoFactura(factura.IdFactura);

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteMovimientoAsync(int id)
        {
            var movimiento = await _context.MovimientosCaja.FindAsync(id);
            if (movimiento == null)
            {
                throw new KeyNotFoundException($"No se encontró un movimiento de caja con ID {id}.");
            }

            movimiento.Deleted = true;
            await _context.SaveChangesAsync();
        }

        // Método auxiliar para actualizar el estado de la factura
        private async Task ActualizarEstadoFactura(int facturaId)
        {
            var factura = await _context.Facturas
                .Include(f => f.MovimientosCaja)
                .FirstOrDefaultAsync(f => f.IdFactura == facturaId);

            if (factura == null) return;

            decimal totalPagado = factura.MovimientosCaja.Sum(m => m.Monto);
            decimal saldoPendiente = factura.Total - totalPagado;

            // Actualizar estado basado en los pagos
            if (saldoPendiente <= 0)
            {
                factura.Estado = EstadoFactura.Pagada;
            }
            else if (totalPagado > 0)
            {
                factura.Estado = EstadoFactura.ParcialmentePagada;
            }
            else
            {
                factura.Estado = EstadoFactura.Emitida;
            }

            await _context.SaveChangesAsync();
        }

        private IQueryable<MovimientoCaja> GetMovimientosQuery()
        {
            return _context.MovimientosCaja
                .Include(m => m.MetodoPago)
    .Include(m => m.Factura)
        .ThenInclude(f => f.Envio)
            .ThenInclude(e => e.Origen)
                .ThenInclude(o => o.Localidad)
                    .ThenInclude(l => l.Provincia)
                        .ThenInclude(p => p.Pais)
    .Include(m => m.Factura)
        .ThenInclude(f => f.Envio)
            .ThenInclude(e => e.Destino)
                .ThenInclude(d => d.Localidad)
                    .ThenInclude(l => l.Provincia)
                        .ThenInclude(p => p.Pais)
    .Include(m => m.Factura)
        .ThenInclude(f => f.Envio)
            .ThenInclude(e => e.Vehiculo)
    .Include(m => m.Factura)
        .ThenInclude(f => f.Envio)
            .ThenInclude(e => e.Conductor)
    .Include(m => m.Factura)
        .ThenInclude(f => f.Envio)
            .ThenInclude(e => e.TipoCarga)
    .Include(m => m.Factura)
        .ThenInclude(f => f.Envio)
            .ThenInclude(e => e.Estado)
    .Include(m => m.Factura)
        .ThenInclude(f => f.Envio)
            .ThenInclude(e => e.Cliente)
    .Include(m => m.Factura)
        .ThenInclude(f => f.Cliente);
        }
    }
}

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
            var query = _context.MovimientosCaja
                .Include(m => m.Factura)
                .Include(m => m.MetodoPago)
                .OrderBy(m => m.FechaPago);

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
            var movimiento = await _context.MovimientosCaja
                .Include(m => m.Factura)
                .FirstOrDefaultAsync(m => m.IdMovimiento == id);

            if (movimiento == null) return null;

           return movimiento.ToDto();
        }

        public async Task CreateMovimientoAsync(CreateMovimientoCajaDto movimientoCajaDto)
        {

            if (movimientoCajaDto == null)
            {
                throw new ArgumentNullException(nameof(movimientoCajaDto), "El movimiento de caja no puede ser nulo.");
            }

            var factura = await _context.Facturas
                .Include(f => f.MovimientosCaja)
                .FirstOrDefaultAsync(f => f.IdFactura == movimientoCajaDto.IdFactura);

            if (factura == null)
            {
                throw new KeyNotFoundException($"No se encontró una factura con ID {movimientoCajaDto.IdFactura}.");
            }

            if (movimientoCajaDto.Monto <= 0)
            {
                throw new ArgumentException("El monto debe ser mayor a cero.", nameof(movimientoCajaDto.Monto));
            }

            // Verificar que no se exceda el monto pendiente
            decimal totalPagadoActual = factura.MovimientosCaja.Sum(m => m.Monto);
            decimal saldoPendienteActual = factura.Total - totalPagadoActual;

            if (movimientoCajaDto.Monto > saldoPendienteActual)
            {
                throw new InvalidOperationException($"El monto a pagar ({movimientoCajaDto.Monto:C}) excede el saldo pendiente ({saldoPendienteActual:C}).");
            }

            var nuevoMovimiento = movimientoCajaDto.ToEntity();

            _context.MovimientosCaja.Add(nuevoMovimiento);

            // Actualizar estado de la factura usando las propiedades NotMapped
            factura.MovimientosCaja.Add(nuevoMovimiento);

            decimal nuevoSaldoPendiente = factura.SaldoPendiente;

            if (nuevoSaldoPendiente <= 0)
                factura.Estado = EstadoFactura.Pagada;
            else if (factura.TotalPagado > 0)
                factura.Estado = EstadoFactura.ParcialmentePagada;
            else
                factura.Estado = EstadoFactura.Emitida;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovimientoAsync(MovimientoCajaDto movimientoCaja)
        {
            if (movimientoCaja == null)
            {
                throw new ArgumentNullException(nameof(movimientoCaja), "El movimiento de caja no puede ser nulo.");
            }
            var movimientoExistente = await _context.MovimientosCaja.FindAsync(movimientoCaja.IdMovimiento);
            if (movimientoExistente == null)
            {
                throw new KeyNotFoundException($"No se encontró un movimiento de caja con ID {movimientoCaja.IdMovimiento}.");
            }

            movimientoExistente.Monto = movimientoCaja.Monto;
            movimientoExistente.FechaPago = movimientoCaja.FechaPago;
            movimientoExistente.IdMetodoPago = movimientoCaja.IdMetodoPago;
            movimientoExistente.Observaciones = movimientoCaja.Observaciones;
            movimientoExistente.IdFactura = movimientoCaja.Factura.IdFactura;

            await _context.SaveChangesAsync();
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
    }
}

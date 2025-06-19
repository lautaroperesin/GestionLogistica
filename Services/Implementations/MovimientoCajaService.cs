using GestionLogisticaBackend.DTOs.Factura;
using GestionLogisticaBackend.DTOs.MovimientoCaja;
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

        public async Task<IEnumerable<MovimientoCajaDto>> GetMovimientosAsync()
        {
            return await _context.MovimientosCaja
                .Include(m => m.Factura)
                    .ThenInclude(f => f.EstadoFactura)
                    .Select(m => new MovimientoCajaDto
                    {
                        IdMovimiento = m.IdMovimiento,
                        Factura = new FacturaDto
                        {
                            IdFactura = m.IdFactura,
                            FechaEmision = m.FechaPago,
                            NumeroFactura = m.Factura.NumeroFactura,
                            EstadoFactura = new EstadoFacturaDto
                            {
                                IdEstadoFactura = m.Factura.EstadoFactura.IdEstadoFactura,
                                Nombre = m.Factura.EstadoFactura.Nombre
                            }
                        },
                        FechaPago = m.FechaPago,
                        Monto = m.Monto,
                        IdMetodoPago = m.IdMetodoPago, // deberia ser un DTO de MetodoPago
                        Observaciones = m.Observaciones    
                    }).ToListAsync();
        }

        public async Task<MovimientoCajaDto> GetMovimientoByIdAsync(int id)
        {
            var movimiento = await _context.MovimientosCaja
                .Include(m => m.Factura)
                    .ThenInclude(f => f.EstadoFactura)
                .FirstOrDefaultAsync(m => m.IdMovimiento == id);

            if (movimiento == null) return null;

            return new MovimientoCajaDto
            {
                IdMovimiento = movimiento.IdMovimiento,
                Factura = new FacturaDto
                {
                    IdFactura = movimiento.IdFactura,
                    FechaEmision = movimiento.FechaPago,
                    NumeroFactura = movimiento.Factura.NumeroFactura,
                    EstadoFactura = new EstadoFacturaDto
                    {
                        IdEstadoFactura = movimiento.Factura.EstadoFactura.IdEstadoFactura,
                        Nombre = movimiento.Factura.EstadoFactura.Nombre
                    }
                },
                FechaPago = movimiento.FechaPago,
                Monto = movimiento.Monto,
                IdMetodoPago = movimiento.IdMetodoPago, // deberia ser un DTO de MetodoPago
                Observaciones = movimiento.Observaciones,
            };
        }

        public async Task CreateMovimientoAsync(CreateMovimientoCajaDto movimientoCaja)
        {
            var factura = await _context.Facturas
                .Include(f => f.MovimientosCaja)
                .FirstOrDefaultAsync(f => f.IdFactura == movimientoCaja.IdFactura);

            if (factura == null)
            {
                throw new KeyNotFoundException($"No se encontró una factura con ID {movimientoCaja.IdFactura}.");
            }

            if (movimientoCaja == null)
            {
                throw new ArgumentNullException(nameof(movimientoCaja), "El movimiento de caja no puede ser nulo.");
            }

            var nuevoMovimiento = new MovimientoCaja
            {
                Monto = movimientoCaja.Monto,
                FechaPago = movimientoCaja.FechaPago,
                IdMetodoPago = movimientoCaja.IdMetodoPago,
                Observaciones = movimientoCaja.Observaciones,
                IdFactura = movimientoCaja.IdFactura,
            };

            _context.MovimientosCaja.Add(nuevoMovimiento);

            decimal montoPagadoPrevio = factura.MovimientosCaja.Sum(m => m.Monto);

            decimal montoPagadoTotal = montoPagadoPrevio + movimientoCaja.Monto;
            decimal saldoPendiente = factura.Total - montoPagadoTotal;

            // Actualizar el estado de la factura según el saldo pendiente
            if (saldoPendiente <= 0)
            {
                factura.IdEstadoFactura = 3; // Pagada
            }
            else if (montoPagadoTotal > 0)
            {
                factura.IdEstadoFactura = 2; // Parcialmente Pagada
            }
            else
            {
                factura.IdEstadoFactura = 1; // Pendiente
            }

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

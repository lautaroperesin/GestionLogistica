using GestionLogisticaBackend.DTOs.MetodoPago;
using GestionLogisticaBackend.Models;
using GestionLogisticaBackend.Services.Interfaces;
using LogisticaBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionLogisticaBackend.Services.Implementations
{
    public class MetodoPagoService : IMetodoPagoService
    {
        private readonly LogisticaContext _context;

        public MetodoPagoService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<List<MetodoPagoDto>> GetMetodosPagoAsync()
        {
            return await _context.MetodosPago.Select(mp => new MetodoPagoDto
            {
                Id = mp.IdMetodoPago,
                Nombre = mp.Nombre
            }).ToListAsync();
        }

        public async Task<MetodoPagoDto> GetMetodoPagoByIdAsync(int id)
        {
            var metodoPago = await _context.MetodosPago.FindAsync(id);

            if (metodoPago == null)
            {
                throw new KeyNotFoundException($"Metodo de pago con ID {id} no encontrado.");
            }

            return new MetodoPagoDto
            {
                Id = metodoPago.IdMetodoPago,
                Nombre = metodoPago.Nombre
            };
        }

        public async Task<MetodoPagoDto> CreateMetodoPagoAsync(MetodoPagoDto metodoPago)
        {
            if (metodoPago == null)
            {
                throw new ArgumentNullException(nameof(metodoPago), "El metodo de pago no puede ser nulo.");
            }

            var nuevoMetodoPago = new MetodoPago
            {
                Nombre = metodoPago.Nombre
            };

            _context.MetodosPago.Add(nuevoMetodoPago);
            await _context.SaveChangesAsync();

            return new MetodoPagoDto
            {
                Id = metodoPago.Id,
                Nombre = metodoPago.Nombre
            };
        }

        public async Task UpdateMetodoPagoAsync(int id, MetodoPagoDto metodoPago)
        {
            if (id != metodoPago.Id)
            {
                throw new ArgumentException("El ID del metodo de pago no coincide.");
            }

            var existingMetodoPago = await _context.MetodosPago.FindAsync(id);

            if (existingMetodoPago == null)
            {
                throw new KeyNotFoundException($"Metodo de pago con ID {id} no encontrado.");
            }

            existingMetodoPago.Nombre = metodoPago.Nombre;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMetodoPagoAsync(int id)
        {
            var metodoPago = await _context.MetodosPago.FindAsync(id);
            if (metodoPago == null)
            {
                throw new KeyNotFoundException($"Metodo de pago con ID {id} no encontrado.");
            }
            _context.MetodosPago.Remove(metodoPago);
            await _context.SaveChangesAsync();
        }
    }
}

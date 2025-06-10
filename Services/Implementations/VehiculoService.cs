using GestionLogisticaBackend.DTOs.Vehiculo;
using GestionLogisticaBackend.Services.Interfaces;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLogisticaBackend.Services.Implementations
{
    public class VehiculoService : IVehiculoService
    {
        private readonly LogisticaContext _context;

        public VehiculoService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<List<VehiculoDto>> GetVehiculosAsync()
        {
            return await _context.Vehiculos
                .Select(v => new VehiculoDto
                {
                    IdVehiculo = v.IdVehiculo,
                    Marca = v.Marca,
                    Modelo = v.Modelo,
                    Patente = v.Patente
                }).ToListAsync();
        }

        public async Task<VehiculoDto?> GetVehiculoByIdAsync(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);

            if (vehiculo == null) return null;

            return new VehiculoDto
            {
                IdVehiculo = vehiculo.IdVehiculo,
                Marca = vehiculo.Marca,
                Modelo = vehiculo.Modelo,
                Patente = vehiculo.Patente
            };
        }

        public async Task<VehiculoDto> CreateVehiculoAsync(CreateVehiculoDto vehiculoDto)
        {
            if (vehiculoDto == null)
            {
                throw new ArgumentNullException(nameof(vehiculoDto), "El vehículo no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(vehiculoDto.Marca) || string.IsNullOrWhiteSpace(vehiculoDto.Modelo) || string.IsNullOrWhiteSpace(vehiculoDto.Patente))
            {
                throw new ArgumentException("El vehículo debe tener una marca, modelo y patente válidos.");
            }

            var vehiculo = new Vehiculo
            {
                Marca = vehiculoDto.Marca,
                Modelo = vehiculoDto.Modelo,
                Patente = vehiculoDto.Patente,
                Refrigerado = false,
                CapacidadKg = vehiculoDto.CapacidadCarga,
                UltimaInspeccion = DateTime.UtcNow
            };

            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();

            return new VehiculoDto
            {
                IdVehiculo = vehiculo.IdVehiculo,
                Marca = vehiculo.Marca,
                Modelo = vehiculo.Modelo,
                Patente = vehiculo.Patente
            };
        }

        public async Task<bool> UpdateVehiculoAsync(UpdateVehiculoDto vehiculoDto)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(vehiculoDto.IdVehiculo);

            if (vehiculo == null)
            {
                throw new KeyNotFoundException($"Vehículo con ID {vehiculoDto.IdVehiculo} no encontrado.");
            }

            if (string.IsNullOrWhiteSpace(vehiculoDto.Marca) || string.IsNullOrWhiteSpace(vehiculoDto.Modelo) || string.IsNullOrWhiteSpace(vehiculoDto.Patente))
            {
                throw new ArgumentException("El vehículo debe tener una marca, modelo y patente válidos.");
            }

            vehiculo.Marca = vehiculoDto.Marca;
            vehiculo.Modelo = vehiculoDto.Modelo;
            vehiculo.Patente = vehiculoDto.Patente;
            vehiculo.CapacidadKg = vehiculoDto.CapacidadCarga;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVehiculoAsync(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);

            if (vehiculo == null) return false;
 
            // Marcar como eliminado en lugar de eliminar físicamente
            vehiculo.Deleted = true;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

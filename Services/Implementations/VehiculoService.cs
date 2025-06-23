using GestionLogisticaBackend.DTOs.Vehiculo;
using GestionLogisticaBackend.Extensions;
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
            var vehiculos = await _context.Vehiculos.ToListAsync();

            return (List<VehiculoDto>)vehiculos.ToDtoList();
        }

        public async Task<VehiculoDto?> GetVehiculoByIdAsync(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);

            if (vehiculo == null) return null;

            return vehiculo.ToDto();
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

            var vehiculo = vehiculoDto.ToEntity();

            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();

            return vehiculo.ToDto();
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

            vehiculo.UpdateFromDto(vehiculoDto);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVehiculoAsync(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);

            if (vehiculo == null) return false;

            vehiculo.Deleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

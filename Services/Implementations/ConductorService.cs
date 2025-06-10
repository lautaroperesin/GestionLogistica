using GestionLogisticaBackend.DTOs.Cliente;
using GestionLogisticaBackend.DTOs.Conductor;
using GestionLogisticaBackend.Services.Interfaces;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLogisticaBackend.Services.Implementations
{
    public class ConductorService : IConductorService
    {
        private readonly LogisticaContext _context;

        public ConductorService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<List<ConductorDto>> GetConductoresAsync()
        {
            return await _context.Conductores
                .Select(c => new ConductorDto
                {
                    IdConductor = c.IdConductor,
                    Dni = c.Dni,
                    Nombre = c.Nombre,
                    Telefono = c.Telefono,
                    Email = c.Email,
                    ClaseLicencia = c.ClaseLicencia,
                    VencimientoLicencia = c.VencimientoLicencia
                }).ToListAsync();
        }

        public async Task<ConductorDto?> GetConductorByIdAsync(int id)
        {
            var conductor = await _context.Conductores.FindAsync(id);

            if (conductor == null) return null;

            return new ConductorDto
            {
                IdConductor = conductor.IdConductor,
                Dni = conductor.Dni,
                Nombre = conductor.Nombre,
                ClaseLicencia = conductor.ClaseLicencia,
                VencimientoLicencia = conductor.VencimientoLicencia,
                Telefono = conductor.Telefono,
                Email = conductor.Email
            };
        }

        public async Task<ConductorDto> CreateConductorAsync(CreateConductorDto conductorDto)
        {
            if (conductorDto == null)
            {
                throw new ArgumentNullException(nameof(conductorDto), "El conductor no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(conductorDto.Nombre) || string.IsNullOrWhiteSpace(conductorDto.Telefono) || string.IsNullOrWhiteSpace(conductorDto.Email))
            {
                throw new ArgumentException("El conductor debe tener un nombre, teléfono y email válidos.");
            }

            var conductor = new Conductor
            {
                Dni = conductorDto.Dni,
                Nombre = conductorDto.Nombre,
                ClaseLicencia = conductorDto.ClaseLicencia,
                VencimientoLicencia = conductorDto.VencimientoLicencia,
                Telefono = conductorDto.Telefono,
                Email = conductorDto.Email
            };

            _context.Conductores.Add(conductor);
            await _context.SaveChangesAsync();

            return new ConductorDto
            {
                IdConductor = conductor.IdConductor,
                Dni = conductor.Dni,
                Nombre = conductor.Nombre,
                ClaseLicencia = conductor.ClaseLicencia,
                VencimientoLicencia = conductor.VencimientoLicencia,
                Telefono = conductor.Telefono,
                Email = conductor.Email
            };
        }

        public async Task<bool> UpdateConductorAsync(UpdateConductorDto conductorDto)
        {
            if (conductorDto == null)
            {
                throw new ArgumentNullException(nameof(conductorDto), "El conductor no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(conductorDto.Nombre) || string.IsNullOrWhiteSpace(conductorDto.Telefono) || string.IsNullOrWhiteSpace(conductorDto.Email))
            {
                throw new ArgumentException("El conductor debe tener un nombre, teléfono y email válidos.");
            }

            var conductor = await _context.Conductores.FindAsync(conductorDto.IdConductor);

            if (conductor == null) return false;

            conductor.Dni = conductorDto.Dni;
            conductor.Nombre = conductorDto.Nombre;
            conductor.ClaseLicencia = conductorDto.ClaseLicencia;
            conductor.VencimientoLicencia = conductorDto.VencimientoLicencia;
            conductor.Telefono = conductorDto.Telefono;
            conductor.Email = conductorDto.Email;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteConductorAsync(int id)
        {
            var conductor = await _context.Conductores.FindAsync(id);

            if (conductor == null) return false;

            conductor.Deleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

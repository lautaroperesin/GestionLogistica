﻿using System.Diagnostics;
using GestionLogisticaBackend.DTOs.Cliente;
using GestionLogisticaBackend.DTOs.Conductor;
using GestionLogisticaBackend.DTOs.Pagination;
using GestionLogisticaBackend.Extensions;
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

        public async Task<PagedResult<ConductorDto>> GetConductoresAsync(PaginationParams pagParams)
        {
            var query = _context.Conductores.OrderBy(c => c.Nombre);

            var totalItems = await query.CountAsync();

            var conductores = await query
                .Skip((pagParams.PageNumber - 1) * pagParams.PageSize)
                .Take(pagParams.PageSize)
                .ToListAsync();

            var conductoresDto = conductores.ToDtoList();

            return new PagedResult<ConductorDto>
            {
                Items = conductoresDto,
                TotalItems = totalItems,
                PageNumber = pagParams.PageNumber,
                PageSize = pagParams.PageSize
            };
        }

        public async Task<ConductorDto?> GetConductorByIdAsync(int id)
        {
            var conductor = await _context.Conductores.FindAsync(id);

            if (conductor == null) return null;

            return conductor.ToDto();
        }

        public async Task<ConductorDto> CreateConductorAsync(CreateConductorDto conductorDto)
        {
            if (conductorDto == null)
            {
                throw new ArgumentNullException(nameof(conductorDto), "El conductor no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(conductorDto.Nombre) ||
                string.IsNullOrWhiteSpace(conductorDto.Dni) || 
                string.IsNullOrWhiteSpace(conductorDto.Telefono) || 
                string.IsNullOrWhiteSpace(conductorDto.Email))
            {
                throw new ArgumentException("El conductor debe tener un nombre, DNI, teléfono y email válidos.");
            }

            // validar si existe un conductor con el mismo dni
            var existe = await _context.Conductores.AnyAsync(c => c.Dni == conductorDto.Dni);
            if (existe)
            {
                throw new ArgumentException($"Ya existe un conductor con el DNI {conductorDto.Dni}.");
            }

            var conductor = conductorDto.ToEntity();

            _context.Conductores.Add(conductor);
            await _context.SaveChangesAsync();

            return conductor.ToDto();
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

            conductor.UpdateFromDto(conductorDto);

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

        public async Task<IEnumerable<ConductorDto>> GetConductoresConLicenciaVencidaAsync()
        {
            var conductoresLicenciaVencida = await _context.Conductores
                .Where(c => c.LicenciaVencida)
                .ToListAsync();

            return conductoresLicenciaVencida.ToDtoList();
        }
    }
}

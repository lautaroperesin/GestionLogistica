using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using GestionLogisticaBackend.Services.Implementations;
using GestionLogisticaBackend.Services.Interfaces;
using GestionLogisticaBackend.DTOs.Vehiculo;

namespace LogisticaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly IVehiculoService _vehiculoService;

        public VehiculosController(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        // GET: api/Vehiculos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehiculoDto>>> GetVehiculos()
        {
            var vehiculos = await _vehiculoService.GetVehiculosAsync();

            return Ok(vehiculos);
        }

        // GET: api/Vehiculos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehiculoDto>> GetVehiculo(int id)
        {
            var vehiculo = await _vehiculoService.GetVehiculoByIdAsync(id);

            if (vehiculo == null) return NotFound();

            return Ok(vehiculo);
        }

        // PUT: api/Vehiculos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehiculo(int id, UpdateVehiculoDto vehiculoDto)
        {
            if (id != vehiculoDto.IdVehiculo)
            {
                return BadRequest("El ID del vehículo no coincide con el ID proporcionado en la URL.");
            }

            try
            {
                var result = await _vehiculoService.UpdateVehiculoAsync(vehiculoDto);
                if (!result)
                {
                    return NotFound("Vehículo no encontrado o no se pudo actualizar.");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // POST: api/Vehiculos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehiculoDto>> PostVehiculo(CreateVehiculoDto vehiculoDto)
        {
            var vehiculo = await _vehiculoService.CreateVehiculoAsync(vehiculoDto);

            return CreatedAtAction("GetVehiculo", new { id = vehiculo.IdVehiculo }, vehiculo);
        }

        // DELETE: api/Vehiculos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiculo(int id)
        {
            var result = await _vehiculoService.DeleteVehiculoAsync(id);

            if (!result) return NotFound();

            return NoContent();
        }
    }
}

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
using GestionLogisticaBackend.DTOs.Conductor;
using GestionLogisticaBackend.Services.Interfaces;

namespace LogisticaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConductoresController : ControllerBase
    {
        private readonly IConductorService _conductorService;

        public ConductoresController(IConductorService conductorService)
        {
            _conductorService = conductorService;
        }

        // GET: api/Conductores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConductorDto>>> GetConductores()
        {
            var conductores = await _conductorService.GetConductoresAsync();

            return Ok(conductores);
        }

        // GET: api/Conductores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConductorDto>> GetConductor(int id)
        {
            var conductor = await _conductorService.GetConductorByIdAsync(id);
            if (conductor == null)
            {
                return NotFound($"Conductor con ID {id} no encontrado.");
            }

            return Ok(conductor);
        }

        // PUT: api/Conductores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConductor(int id, UpdateConductorDto conductorDto)
        {
            if (id != conductorDto.IdConductor)
            {
                return BadRequest("El ID del conductor no coincide con el ID proporcionado en la URL.");
            }
            try
            {
                var result = await _conductorService.UpdateConductorAsync(conductorDto);
                if (!result)
                {
                    return NotFound($"Conductor con ID {id} no encontrado.");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // POST: api/Conductores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConductorDto>> PostConductor(CreateConductorDto conductorDto)
        {
            var nuevoConductor = await _conductorService.CreateConductorAsync(conductorDto);

            return CreatedAtAction("GetConductor", new { id = nuevoConductor.IdConductor }, nuevoConductor);
        }

        // DELETE: api/Conductores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConductor(int id)
        {
            var conductor = await _conductorService.GetConductorByIdAsync(id);

            if (conductor == null)
            {
                return NotFound($"Conductor con ID {id} no encontrado.");
            }

            var result = await _conductorService.DeleteConductorAsync(id);

            if (!result)
            {
                return NotFound($"Conductor con ID {id} no encontrado o no se pudo eliminar.");
            }

            return NoContent();
        }
    }
}

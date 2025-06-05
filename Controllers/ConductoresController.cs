using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using GestionLogisticaBackend.Services;

namespace LogisticaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConductoresController : ControllerBase
    {
        private readonly ConductorService _conductorService;

        public ConductoresController(ConductorService conductorService)
        {
            _conductorService = conductorService;
        }

        // GET: api/Conductores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conductor>>> GetConductores()
        {
            return await _conductorService.GetConductoresAsync();
        }

        // GET: api/Conductores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Conductor>> GetConductor(int id)
        {
            var conductor = await _conductorService.GetConductorByIdAsync(id);
            if (conductor == null)
            {
                return NotFound();
            }
            return conductor;
        }

        // PUT: api/Conductores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConductor(int id, Conductor conductor)
        {
            if (id != conductor.IdConductor)
            {
                return BadRequest("El ID del conductor no coincide.");
            }
            try
            {
                conductor = await _conductorService.UpdateConductorAsync(conductor);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _conductorService.GetConductorByIdAsync(id) == null)
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        // POST: api/Conductores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Conductor>> PostConductor(Conductor conductor)
        {
            try
            {
                conductor = await _conductorService.CreateConductorAsync(conductor);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction("GetConductor", new { id = conductor.IdConductor }, conductor);
        }

        // DELETE: api/Conductores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConductor(int id)
        {
            try
            {
                var result = await _conductorService.DeleteConductorAsync(id);
                if (!result)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}

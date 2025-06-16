using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using GestionLogisticaBackend.Services.Interfaces;
using GestionLogisticaBackend.DTOs.Envio;

namespace GestionLogisticaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviosController : ControllerBase
    {
        private readonly IEnvioService _envioService;

        public EnviosController(IEnvioService envioService)
        {
            _envioService = envioService;
        }

        // GET: api/Envios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnvioDto>>> GetEnvios()
        {
            var envios = await _envioService.GetAllEnviosAsync();
            if (envios == null || !envios.Any())
            {
                return NotFound("No se encontraron envíos.");
            }
            return Ok(envios);
        }

        // GET: api/Envios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Envio>> GetEnvio(int id)
        {
            var envio = await _envioService.GetEnvioByIdAsync(id);
            if (envio == null)
            {
                return NotFound($"No se encontró el envío con ID {id}.");
            }
            return Ok(envio);
        }

        // PUT: api/Envios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnvio(int id, UpdateEnvioDto envioDto)
        {
            if (id != envioDto.IdEnvio)
            {
                return BadRequest("El ID del envío en la URL no coincide con el del cuerpo.");
            }

            var updatedEnvio = await _envioService.UpdateEnvioAsync(id, envioDto);

            if (updatedEnvio == null)
            {
                return NotFound($"Envío con ID {id} no encontrado.");
            }

            return NoContent();
        }

        // POST: api/Envios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Envio>> PostEnvio(CreateEnvioDto envioDto)
        {
            if (envioDto == null)
            {
                return BadRequest("Datos de envío inválidos.");
            }

            var createdEnvio = await _envioService.CreateEnvioAsync(envioDto);
            if (createdEnvio == null)
            {
                return BadRequest("Error al crear el envío.");
            }

            return CreatedAtAction(nameof(GetEnvio), new { id = createdEnvio.IdEnvio }, createdEnvio);
        }

        // DELETE: api/Envios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnvio(int id)
        {
            var deleted = await _envioService.DeleteEnvioAsync(id);

            if (!deleted)
            {
                return BadRequest("Error al eliminar el envío.");
            }

            return NoContent();
        }
    }
}

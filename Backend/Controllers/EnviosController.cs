using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionLogisticaBackend.DTOs.Envio;
using GestionLogisticaBackend.DTOs.Filters;
using GestionLogisticaBackend.DTOs.Pagination;
using GestionLogisticaBackend.Enums;
using GestionLogisticaBackend.Services.Interfaces;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionLogisticaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnviosController : ControllerBase
    {
        private readonly IEnvioService _envioService;

        public EnviosController(IEnvioService envioService)
        {
            _envioService = envioService;
        }

        // GET: api/Envios
        [HttpGet]
        public async Task<ActionResult<PagedResult<EnvioDto>>> GetEnvios([FromQuery] EnvioFilterDto filtros)
        {
            var envios = await _envioService.GetEnviosAsync(filtros);

            return Ok(envios);
        }

        // GET: api/Envios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnvioDto>> GetEnvio(int id)
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
        public async Task<ActionResult<EnvioDto>> PostEnvio(CreateEnvioDto envioDto)
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

        // actualizar estado del envio
        [HttpPatch("{id}/estado/")]
        public async Task<IActionResult> UpdateEnvioEstado(int id, [FromBody] EstadoEnvioEnum nuevoEstado)
        {
            try
            {
                await _envioService.UpdateEnvioEstadoAsync(id, nuevoEstado);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el estado del envío: {ex.Message}");
            }
        }
    }
}

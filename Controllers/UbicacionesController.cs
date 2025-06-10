using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using GestionLogisticaBackend.DTOs.Ubicacion;
using GestionLogisticaBackend.Services.Implementations;
using GestionLogisticaBackend.Services.Interfaces;

namespace LogisticaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionesController : ControllerBase
    {
        private readonly IUbicacionService _ubicacionService;

        public UbicacionesController(IUbicacionService ubicacionService)
        {
            _ubicacionService = ubicacionService;
        }

        // GET: api/Ubicaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UbicacionDto>>> GetUbicaciones()
        {
            var ubicaciones = await _ubicacionService.GetUbicacionesAsync();
            
            return Ok(ubicaciones);
        }

        // GET: api/Ubicaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UbicacionDto>> GetUbicacion(int id)
        {
            var ubicacion = await _ubicacionService.GetUbicacionByIdAsync(id);

            if (ubicacion == null)
            {
                return NotFound();
            }

            return ubicacion;
        }

        // PUT: api/Ubicaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUbicacion(int id, UpdateUbicacionDto ubicacionDto)
        {
            if (id != ubicacionDto.IdUbicacion)
            {
                return BadRequest("El ID de la ubicación no coincide con el ID proporcionado en la URL.");
            }

            var updated = await _ubicacionService.UpdateUbicacionAsync(ubicacionDto);

            if (!updated)
            {
                return NotFound("No se encontró la ubicación para actualizar.");
            }

            return NoContent();
        }

        // POST: api/Ubicaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostUbicacion(CreateUbicacionDto ubicacionDto)
        {
            var nuevaUbicacion = await _ubicacionService.CreateUbicacionAsync(ubicacionDto);

            return CreatedAtAction("GetUbicacion", new { id = nuevaUbicacion.IdUbicacion }, nuevaUbicacion);
        }

        // DELETE: api/Ubicaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUbicacion(int id)
        {
            var ubicacionBorrada = await _ubicacionService.DeleteUbicacionAsync(id);

            if (!ubicacionBorrada) return NotFound();

            return NoContent();
        }
    }
}

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
using GestionLogisticaBackend.Services;

namespace LogisticaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionesController : ControllerBase
    {
        private readonly UbicacionService _ubicacionService;

        public UbicacionesController(UbicacionService ubicacionService)
        {
            _ubicacionService = ubicacionService;
        }

        // GET: api/Ubicaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UbicacionDto>>> GetUbicaciones()
        {
            var ubicaciones = await _ubicacionService.GetUbicacionesAsync();

            var ubicacionesDto = ubicaciones
                .Select(u => new UbicacionDto
                {
                    IdUbicacion = u.IdUbicacion,
                    Direccion = u.Direccion,
                    IdLocalidad = u.IdLocalidad,
                    LocalidadNombre = u.Localidad.Nombre
                });
            
            return Ok(ubicacionesDto);
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

            var ubicacionDto = new UbicacionDto
            {
                IdUbicacion = ubicacion.IdUbicacion,
                Direccion = ubicacion.Direccion,
                IdLocalidad = ubicacion.IdLocalidad,
                LocalidadNombre = ubicacion.Localidad.Nombre
            };

            return ubicacionDto;
        }

        // PUT: api/Ubicaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUbicacion(int id, CreateUbicacionDto dto)
        {
            var ubicacion = await _ubicacionService.GetUbicacionByIdAsync(id);

            ubicacion.Direccion = dto.Direccion;
            ubicacion.IdLocalidad = dto.IdLocalidad;

            try
            {
                await _ubicacionService.UpdateUbicacionAsync(ubicacion);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _ubicacionService.GetUbicacionByIdAsync(ubicacion.IdUbicacion) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Ubicaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostUbicacion(CreateUbicacionDto dto)
        {
            var ubicacion = new Ubicacion
            {
                Direccion = dto.Direccion,
                IdLocalidad = dto.IdLocalidad
            };

            await _ubicacionService.CreateUbicacionAsync(ubicacion);

            return CreatedAtAction("GetUbicacion", new { id = ubicacion.IdUbicacion }, dto);
        }

        // DELETE: api/Ubicaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUbicacion(int id)
        {
            var ubicacionBorrada = await _ubicacionService.DeleteUbicacionAsync(id);

            if (!ubicacionBorrada)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

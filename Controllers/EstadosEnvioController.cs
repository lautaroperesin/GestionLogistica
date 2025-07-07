using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using GestionLogisticaBackend.DTOs.Envio;

namespace LogisticaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosEnvioController : ControllerBase
    {
        private readonly LogisticaContext _context;

        public EstadosEnvioController(LogisticaContext context)
        {
            _context = context;
        }

        // GET: api/EstadosEnvio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoEnvioDto>>> GetEstadosEnvio()
        {
            var estadosEnvio = await _context.EstadosEnvio.ToListAsync();

            return estadosEnvio.Select(e => new EstadoEnvioDto
            {
                IdEstado = e.IdEstado,
                Nombre = e.Nombre
            }).ToList();
        }

        // GET: api/EstadosEnvio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoEnvioDto>> GetEstadoEnvio(int id)
        {
            var estadoEnvio = await _context.EstadosEnvio.FindAsync(id);

            if (estadoEnvio == null)
            {
                return NotFound();
            }

            return new EstadoEnvioDto
            {
                IdEstado = estadoEnvio.IdEstado,
                Nombre = estadoEnvio.Nombre
            };
        }

        // PUT: api/EstadosEnvio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoEnvio(int id, EstadoEnvioDto estadoEnvioDto)
        {
            if (id != estadoEnvioDto.IdEstado)
            {
                return BadRequest();
            }

            var estadoEnvio = await _context.EstadosEnvio.FindAsync(id);

            if (estadoEnvio == null)
            {
                return NotFound();
            }

            estadoEnvio.Nombre = estadoEnvioDto.Nombre;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/EstadosEnvio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstadoEnvio>> PostEstadoEnvio(EstadoEnvioDto dto)
        {
            var estadoEnvio = new EstadoEnvio
            {
                Nombre = dto.Nombre
            };

            _context.EstadosEnvio.Add(estadoEnvio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadoEnvio", new { id = estadoEnvio.IdEstado }, estadoEnvio);
        }

        // DELETE: api/EstadosEnvio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadoEnvio(int id)
        {
            var estadoEnvio = await _context.EstadosEnvio.FindAsync(id);
            if (estadoEnvio == null)
            {
                return NotFound();
            }

            _context.EstadosEnvio.Remove(estadoEnvio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

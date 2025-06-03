using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogisticaBackend.Data;
using LogisticaBackend.Models;

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
        public async Task<ActionResult<IEnumerable<EstadoEnvio>>> GetEstadosEnvio()
        {
            return await _context.EstadosEnvio.ToListAsync();
        }

        // GET: api/EstadosEnvio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoEnvio>> GetEstadoEnvio(int id)
        {
            var estadoEnvio = await _context.EstadosEnvio.FindAsync(id);

            if (estadoEnvio == null)
            {
                return NotFound();
            }

            return estadoEnvio;
        }

        // PUT: api/EstadosEnvio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoEnvio(int id, EstadoEnvio estadoEnvio)
        {
            if (id != estadoEnvio.IdEstado)
            {
                return BadRequest();
            }

            _context.Entry(estadoEnvio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoEnvioExists(id))
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

        // POST: api/EstadosEnvio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstadoEnvio>> PostEstadoEnvio(EstadoEnvio estadoEnvio)
        {
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

        private bool EstadoEnvioExists(int id)
        {
            return _context.EstadosEnvio.Any(e => e.IdEstado == id);
        }
    }
}

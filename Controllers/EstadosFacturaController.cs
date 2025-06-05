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
    public class EstadosFacturaController : ControllerBase
    {
        private readonly LogisticaContext _context;

        public EstadosFacturaController(LogisticaContext context)
        {
            _context = context;
        }

        // GET: api/EstadosFactura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoFactura>>> GetEstadosFactura()
        {
            return await _context.EstadosFactura.ToListAsync();
        }

        // GET: api/EstadosFactura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoFactura>> GetEstadoFactura(int id)
        {
            var estadoFactura = await _context.EstadosFactura.FindAsync(id);

            if (estadoFactura == null)
            {
                return NotFound();
            }

            return estadoFactura;
        }

        // PUT: api/EstadosFactura/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoFactura(int id, EstadoFactura estadoFactura)
        {
            if (id != estadoFactura.IdEstadoFactura)
            {
                return BadRequest();
            }

            _context.Entry(estadoFactura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoFacturaExists(id))
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

        // POST: api/EstadosFactura
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstadoFactura>> PostEstadoFactura(EstadoFactura estadoFactura)
        {
            _context.EstadosFactura.Add(estadoFactura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadoFactura", new { id = estadoFactura.IdEstadoFactura }, estadoFactura);
        }

        // DELETE: api/EstadosFactura/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadoFactura(int id)
        {
            var estadoFactura = await _context.EstadosFactura.FindAsync(id);
            if (estadoFactura == null)
            {
                return NotFound();
            }

            _context.EstadosFactura.Remove(estadoFactura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadoFacturaExists(int id)
        {
            return _context.EstadosFactura.Any(e => e.IdEstadoFactura == id);
        }
    }
}

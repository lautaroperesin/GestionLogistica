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
    public class TiposCargaController : ControllerBase
    {
        private readonly LogisticaContext _context;

        public TiposCargaController(LogisticaContext context)
        {
            _context = context;
        }

        // GET: api/TiposCarga
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoCarga>>> GetTiposCarga()
        {
            return await _context.TiposCarga.ToListAsync();
        }

        // GET: api/TiposCarga/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoCarga>> GetTipoCarga(int id)
        {
            var tipoCarga = await _context.TiposCarga.FindAsync(id);

            if (tipoCarga == null)
            {
                return NotFound();
            }

            return tipoCarga;
        }

        // PUT: api/TiposCarga/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoCarga(int id, TipoCarga tipoCarga)
        {
            if (id != tipoCarga.IdTipoCarga)
            {
                return BadRequest();
            }

            _context.Entry(tipoCarga).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoCargaExists(id))
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

        // POST: api/TiposCarga
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoCarga>> PostTipoCarga(TipoCarga tipoCarga)
        {
            _context.TiposCarga.Add(tipoCarga);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoCarga", new { id = tipoCarga.IdTipoCarga }, tipoCarga);
        }

        // DELETE: api/TiposCarga/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoCarga(int id)
        {
            var tipoCarga = await _context.TiposCarga.FindAsync(id);
            if (tipoCarga == null)
            {
                return NotFound();
            }

            _context.TiposCarga.Remove(tipoCarga);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoCargaExists(int id)
        {
            return _context.TiposCarga.Any(e => e.IdTipoCarga == id);
        }
    }
}

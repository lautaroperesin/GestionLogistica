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
    public class TiposCargaController : ControllerBase
    {
        private readonly LogisticaContext _context;

        public TiposCargaController(LogisticaContext context)
        {
            _context = context;
        }

        // GET: api/TiposCarga
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoCargaDto>>> GetTiposCarga()
        {
            var tiposCarga =  await _context.TiposCarga.ToListAsync();

            return tiposCarga.Select(tc => new TipoCargaDto
            {
                IdTipoCarga = tc.IdTipoCarga,
                Nombre = tc.Nombre
            }).ToList();
        }

        // GET: api/TiposCarga/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoCargaDto>> GetTipoCarga(int id)
        {
            var tipoCarga = await _context.TiposCarga.FindAsync(id);

            if (tipoCarga == null)
            {
                return NotFound();
            }

            return new TipoCargaDto
            {
                IdTipoCarga = tipoCarga.IdTipoCarga,
                Nombre = tipoCarga.Nombre
            };
        }

        // PUT: api/TiposCarga/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoCarga(int id, TipoCargaDto dto)
        {
            if (id != dto.IdTipoCarga)
                return BadRequest();

            var tipoCarga = await _context.TiposCarga.FindAsync(id);

            if (tipoCarga == null)
                return NotFound();

            tipoCarga.Nombre = dto.Nombre;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/TiposCarga
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoCargaDto>> PostTipoCarga(TipoCargaDto dto)
        {
            if (dto == null)
                return BadRequest("El tipo de carga no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest("El nombre del tipo de carga no puede estar vacío.");

            // mapear a entidad
            var tipoCarga = new TipoCarga
            {
                Nombre = dto.Nombre
            };

            _context.TiposCarga.Add(tipoCarga);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTipoCarga), new { id = tipoCarga.IdTipoCarga }, tipoCarga);
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
    }
}

using GestionLogisticaBackend.DTOs.MovimientoCaja;
using GestionLogisticaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionLogisticaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosCajaController : ControllerBase
    {
        private readonly IMovimientoCajaService _movimientoCajaService;

        public MovimientosCajaController(IMovimientoCajaService movimientoCajaService)
        {
            _movimientoCajaService = movimientoCajaService;
        }

        // GET: api/MovimientosCaja
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoCajaDto>>> GetMovimientos()
        {
            var movimientos = await _movimientoCajaService.GetMovimientosAsync();

            return Ok(movimientos);
        }

        // GET: api/MovimientosCaja/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoCajaDto>> GetMovimiento(int id)
        {
            var movimiento = await _movimientoCajaService.GetMovimientoByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound($"No se encontró el movimiento de caja con ID {id}.");
            }
            return Ok(movimiento);
        }

        // POST: api/MovimientosCaja
        [HttpPost]
        public async Task<IActionResult> PostMovimiento(CreateMovimientoCajaDto movimientoCaja)
        {
            if (movimientoCaja == null)
            {
                return BadRequest("El movimiento de caja no puede ser nulo.");
            }
            await _movimientoCajaService.CreateMovimientoAsync(movimientoCaja);

            return CreatedAtAction(nameof(GetMovimiento), new { id = movimientoCaja.IdMetodoPago }, movimientoCaja);
        }

        // PUT: api/MovimientosCaja/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimiento(int id, MovimientoCajaDto movimientoCaja)
        {
            if (id != movimientoCaja.IdMovimiento)
            {
                return BadRequest("El ID del movimiento de caja no coincide con el ID proporcionado en la URL.");
            }

            var existingMovimiento = await _movimientoCajaService.GetMovimientoByIdAsync(id);
            if (existingMovimiento == null)
            {
                return NotFound($"No se encontró el movimiento de caja con ID {id}.");
            }

            await _movimientoCajaService.UpdateMovimientoAsync(movimientoCaja);
            return NoContent();
        }

        // DELETE: api/MovimientosCaja/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            await _movimientoCajaService.DeleteMovimientoAsync(id);
            return NoContent();
        }
    }
}

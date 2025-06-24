using GestionLogisticaBackend.DTOs.MetodoPago;
using GestionLogisticaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionLogisticaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodosPagoController : ControllerBase
    {
        private readonly IMetodoPagoService _service;

        public MetodosPagoController(IMetodoPagoService service)
        {
            _service = service;
        }

        // GET: api/MetodosPago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetodoPagoDto>>> GetMetodosPago()
        {
            var metodosPago = await _service.GetMetodosPagoAsync();
            return Ok(metodosPago);
        }

        // GET: api/MetodosPago/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MetodoPagoDto>> GetMetodoPago(int id)
        {
            var metodoPago = await _service.GetMetodoPagoByIdAsync(id);
            if (metodoPago == null)
            {
                return NotFound();
            }
            return Ok(metodoPago);
        }

        // POST: api/MetodosPago
        [HttpPost]
        public async Task<ActionResult<MetodoPagoDto>> CreateMetodoPago(MetodoPagoDto metodoPagoDto)
        {
            if (metodoPagoDto == null)
            {
                return BadRequest("Metodo de pago no puede ser nulo.");
            }
            var createdMetodoPago = await _service.CreateMetodoPagoAsync(metodoPagoDto);
            return CreatedAtAction(nameof(GetMetodoPago), new { id = createdMetodoPago.Id }, createdMetodoPago);
        }

        // PUT: api/MetodosPago/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMetodoPago(int id, MetodoPagoDto metodoPagoDto)
        {
            if (id != metodoPagoDto.Id)
            {
                return BadRequest("ID del metodo de pago no coincide.");
            }

            await _service.UpdateMetodoPagoAsync(id, metodoPagoDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetodoPago(int id)
        {
            var metodoPago = await _service.GetMetodoPagoByIdAsync(id);
            if (metodoPago == null)
            {
                return NotFound();
            }
            await _service.DeleteMetodoPagoAsync(id);
            return NoContent();
        }
    }
}

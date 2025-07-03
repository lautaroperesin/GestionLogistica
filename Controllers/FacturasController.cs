using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using GestionLogisticaBackend.Services.Interfaces;
using GestionLogisticaBackend.DTOs.Factura;
using GestionLogisticaBackend.DTOs.Pagination;

namespace GestionLogisticaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturaService _facturaService;

        public FacturasController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        // GET: api/Facturas
        [HttpGet]
        public async Task<ActionResult<PagedResult<FacturaDto>>> GetFacturas([FromQuery] PaginationParams pagParams)
        {
            var facturas = await _facturaService.GetFacturasAsync(pagParams);

            return Ok(facturas);
        }

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacturaDto>> GetFactura(int id)
        {
            var factura = await _facturaService.GetFacturaByIdAsync(id);

            if (factura == null)
            {
                return NotFound($"Factura con ID {id} no encontrada.");
            }

            return Ok(factura);
        }

        // PUT: api/Facturas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<FacturaDto>> PutFactura(int id, UpdateFacturaDto facturaDto)
        {
            if (id != facturaDto.IdFactura)
            {
                return BadRequest("El ID de la factura no coincide con el ID proporcionado en el cuerpo de la solicitud.");
            }
            var facturaActualizada = await _facturaService.UpdateFacturaAsync(id, facturaDto);
            if (facturaActualizada == null)
            {
                return NotFound($"Factura con ID {id} no encontrada o no se pudo actualizar.");
            }
            return Ok(facturaActualizada);
        }

        // POST: api/Facturas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FacturaDto>> PostFactura(CreateFacturaDto facturaDto)
        {
            var nuevaFactura = await _facturaService.CreateFacturaAsync(facturaDto);

            if (nuevaFactura == null)
            {
                return BadRequest("Error al crear la factura.");
            }

            return CreatedAtAction(nameof(GetFactura), new { id = nuevaFactura.IdFactura }, nuevaFactura);
        }

        // DELETE: api/Facturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            var eliminado = await _facturaService.DeleteFacturaAsync(id);

            if (!eliminado)
            {
                return NotFound($"Factura con ID {id} no encontrada o no se pudo eliminar.");
            }

            return NoContent();
        }
    }
}

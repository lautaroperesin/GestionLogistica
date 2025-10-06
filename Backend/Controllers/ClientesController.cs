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
using GestionLogisticaBackend.DTOs.Cliente;
using GestionLogisticaBackend.DTOs.Pagination;
using Microsoft.AspNetCore.Authorization;

namespace LogisticaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<PagedResult<ClienteDto>>> GetClientes([FromQuery] PaginationParams pagParams)
        {
            var clientes = await _clienteService.GetClientesAsync(pagParams);

            return Ok(clientes);
        }   

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetCliente(int id)
        {
            var cliente = await _clienteService.GetClienteByIdAsync(id);

            if (cliente == null) return NotFound();

            return Ok(cliente);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, UpdateClienteDto cliente)
        {
            if (id != cliente.IdCliente)
            {
                return BadRequest("El ID del cliente no coincide con el ID proporcionado en la URL.");
            }

            var updated = await _clienteService.UpdateClienteAsync(cliente);

            if (!updated)
            {
                return NotFound("Cliente no encontrado o no se pudo actualizar.");
            }

            return NoContent();
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<ClienteDto>> PostCliente(CreateClienteDto clienteDto)
        {
            var nuevoCliente = await _clienteService.CreateClienteAsync(clienteDto);

            return CreatedAtAction(nameof(GetCliente), new { id = nuevoCliente.IdCliente }, nuevoCliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            await _clienteService.DeleteClienteAsync(id);

            return NoContent();
        }

        // GET: api/Clientes/eliminados
        [HttpGet("eliminados")]
        public async Task<ActionResult<PagedResult<ClienteDto>>> GetClientesEliminados()
        {
            var clientesEliminados = await _clienteService.GetClientesEliminadosAsync();
            return Ok(clientesEliminados);
        }

        // restaurar cliente
        [HttpPost("restore/{id}")]
        public async Task<IActionResult> RestoreCliente(int id)
        {
            var restored = await _clienteService.RestoreClienteAsync(id);
            if (!restored)
            {
                return NotFound("Cliente no encontrado o no se pudo restaurar.");
            }
            return NoContent();
        }
    }
}

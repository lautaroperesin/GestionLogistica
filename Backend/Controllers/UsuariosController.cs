using GestionLogisticaBackend.DTOs.Usuario;
using GestionLogisticaBackend.Models;
using GestionLogisticaBackend.Services;
using GestionLogisticaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;

namespace GestionLogisticaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // get usuario by email usando service
        [HttpGet("byemail")]
        public async Task<ActionResult<UsuarioDto>> GetUserByEmail([FromQuery] string? email)
        {
            try
            {
                var user = await _usuarioService.GetUserByEmailAsync(email);
                if (user != null)
                {
                    return Ok(user);
                }
                return NotFound(new { message = "Usuario no encontrado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al obtener el usuario. Error: {ex.Message}" });
            }
        }
    }
}

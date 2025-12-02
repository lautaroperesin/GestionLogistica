using GestionLogisticaBackend.DTOs.Usuario;
using GestionLogisticaBackend.DTOs.Vehiculo;
using GestionLogisticaBackend.Implementations;
using GestionLogisticaBackend.Models;
using GestionLogisticaBackend.Services;
using GestionLogisticaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Enums;
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

        // get all usuarios conductores
        [HttpGet]
        public async Task<ActionResult<List<UsuarioDto>>> GetUsuariosConductores()
        {
            var usuarios = await _usuarioService.GetUsuariosConductoresAsync();

            return Ok(usuarios);
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

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(CreateUsuarioDto usuario)
        {
            try
            {
                await _usuarioService.AddAsync(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al crear el usuario. Error: {ex.Message}" });
            }

            var usuarioCreado = await _usuarioService.GetUserByEmailAsync(usuario.Email);

            if (usuarioCreado == null)
            {
                // If we couldn't retrieve the created user, return a generic OK to avoid CreatedAtAction routing errors
                return Ok(usuario);
            }

            // Use the existing GetUserByEmail action as the location for CreatedAtAction
            return CreatedAtAction(nameof(GetUserByEmail), new { email = usuarioCreado.Email }, usuarioCreado);
        }

        [HttpPost("login")]
        public async Task<ActionResult<bool>> LoginInSystem([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                var isValidUser = await _usuarioService.LoginInSystem(usuarioDto.Email, usuarioDto.Password);
                if (isValidUser)
                {
                    return Ok(true);
                }
                return Unauthorized(new { message = "Credenciales inválidas." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al iniciar sesión. Error: {ex.Message}" });
            }
        }

        // get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetById(int id)
        {
            try
            {
                var user = await _usuarioService.GetByIdAsync(id);
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

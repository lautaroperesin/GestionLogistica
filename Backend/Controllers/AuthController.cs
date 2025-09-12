using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestionLogisticaBackend.DTOs.Auth;
using GestionLogisticaBackend.DTOs.Usuario;
using GestionLogisticaBackend.Enums;
using GestionLogisticaBackend.Models;
using GestionLogisticaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GestionLogisticaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Usuario>> Register(CreateUsuarioDto usuarioDto)
        {
            var user = await _authService.RegisterAsync(usuarioDto);
            if (user is null)
                return BadRequest("El usuario ya existe o hubo un error al registrarlo.");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UsuarioDto usuarioDto)
        {
            var result = await _authService.LoginAsync(usuarioDto);
            if (result is null)
                return Unauthorized("Credenciales inválidas.");

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto refreshTokenRequest)
        {
            var result = await _authService.RefreshTokensAsync(refreshTokenRequest);
            if (result is null || result.AccessToken is null || result.RefreshToken is null)
                return Unauthorized("Refresh token inválido o expirado.");

            return Ok(result);
        }
    }
}

using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GestionLogisticaBackend.DTOs.Auth;
using GestionLogisticaBackend.DTOs.Usuario;
using GestionLogisticaBackend.Enums;
using GestionLogisticaBackend.Models;
using GestionLogisticaBackend.Services.Interfaces;
using LogisticaBackend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GestionLogisticaBackend.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly LogisticaContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(LogisticaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<TokenResponseDto?> LoginAsync(UsuarioDto usuarioDto)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == usuarioDto.Email);

            if (user is null)
                return null;

            if (new PasswordHasher<Usuario>()
                .VerifyHashedPassword(user, user.PasswordHash, usuarioDto.Password)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return await CreateTokenResponse(user);
        }

        private async Task<TokenResponseDto> CreateTokenResponse(Usuario user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };
        }

        public async Task<Usuario?> RegisterAsync(CreateUsuarioDto usuarioDto)
        {
            if(await _context.Usuarios.AnyAsync(u => u.Email == usuarioDto.Email))
            {
                return null; // Email already exists
            }

            var user = new Usuario();

            var hashedPassword = new PasswordHasher<Usuario>()
                .HashPassword(user, usuarioDto.Password);

            user.Nombre = usuarioDto.Nombre;
            user.Email = usuarioDto.Email;
            user.PasswordHash = hashedPassword;
            user.Activo = true;
            user.UltimoAcceso = DateTime.Now;
            user.FechaAlta = DateTime.Now;

            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto refreshTokenRequest)
        {
            var user = await ValidateRefreshTokenAsync(refreshTokenRequest.UserId, refreshTokenRequest.RefreshToken);
            if (user is null)
                return null;

            return await CreateTokenResponse(user);
        }

        private async Task<Usuario?> ValidateRefreshTokenAsync(int userId, string refreshToken)
        {
            var user = await _context.Usuarios.FindAsync(userId);
            if (user is null || user.RefreshToken != refreshToken || user.FechaExpiracionRefreshToken <= DateTime.Now)
            {
                return null;
            }

            return user;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(Usuario user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.FechaExpiracionRefreshToken = DateTime.Now.AddDays(7);
            await _context.SaveChangesAsync();
            return refreshToken;
        }

        private string CreateToken(Usuario user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, user.RolUsuario),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Token")!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                audience: _configuration.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}

using GestionLogisticaBackend.DTOs.Auth;
using GestionLogisticaBackend.DTOs.Usuario;
using GestionLogisticaBackend.Models;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Usuario?> RegisterAsync(CreateUsuarioDto usuarioDto);
        Task<TokenResponseDto?> LoginAsync(UsuarioDto usuarioDto);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto refreshTokenRequest);
    }
}

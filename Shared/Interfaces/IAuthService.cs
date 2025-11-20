using GestionLogisticaBackend.DTOs.Usuario;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> Login(UsuarioDto login);
        Task<bool> ResetPassword(UsuarioDto? login);
        Task<bool> CreateUserWithEmailAndPasswordAsync(string email, string password, string nombre);
    }
}

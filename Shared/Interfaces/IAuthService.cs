using GestionLogisticaBackend.DTOs.Usuario;

namespace GestionLogisticaBackend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Login(UsuarioDto login);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionLogisticaBackend.DTOs.Usuario;
using Service.Interfaces;

namespace Shared.Interfaces
{
    public interface IUsuarioService
    {
        public Task<UsuarioDto?> GetUserByEmailAsync(string email);
        public Task<UsuarioDto?> GetByIdAsync(int id);
        public Task<UsuarioDto> AddAsync(CreateUsuarioDto createUsuarioDto);
        public Task<bool> LoginInSystem(string email, string password);
    }
}

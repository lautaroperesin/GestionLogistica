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
    }
}

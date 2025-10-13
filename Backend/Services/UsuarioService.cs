using GestionLogisticaBackend.DTOs.Usuario;
using GestionLogisticaBackend.Models;
using LogisticaBackend.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace GestionLogisticaBackend.Services
{
    public class UsuarioService : IUsuarioService
    {
        LogisticaContext _context;

        public UsuarioService(LogisticaContext context)
        {
            _context = context;
        }

        // get usuario by email
        public async Task<UsuarioDto?> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

            // mapear a UsuarioDto
            if (usuario == null)
            {
                return null;
            }

            return new UsuarioDto
            {
                Id = usuario.IdUsuario,
                Email = usuario.Email,
                Password = usuario.Password,
            };
        }

        // add usuario
        public async Task AddAsync(CreateUsuarioDto createUsuarioDto)
        {
            var usuario = new Usuario
            {
                Nombre = createUsuarioDto.Nombre,
                Email = createUsuarioDto.Email,
                Password = createUsuarioDto.Password,
                TipoRol = createUsuarioDto.Rol
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }
    }
}

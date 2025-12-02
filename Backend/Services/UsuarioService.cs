using System.Security.Cryptography;
using System.Text;
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
                TipoRol = usuario.TipoRol
            };
        }

        // add usuario
        public async Task<UsuarioDto> AddAsync(CreateUsuarioDto createUsuarioDto)
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

            return new UsuarioDto
            {
                Id = usuario.IdUsuario,
                Email = usuario.Email,
                Password = usuario.Password,
            };
        }

        public async Task<bool> LoginInSystem(string email, string password)
        {
            var usuario = await _context.Usuarios.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == GetHashSha256(password));

            if (usuario == null)
            {
                return false;
            }

            return usuario != null;
        }

        // get by id
        public async Task<UsuarioDto?> GetByIdAsync(int id)
        {
            var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.IdUsuario == id);
            if (usuario == null)
            {
                return null;
            }
            return new UsuarioDto
            {
                Id = usuario.IdUsuario,
                Email = usuario.Email,
                Password = usuario.Password,
                TipoRol = usuario.TipoRol
            };
        }

        public string GetHashSha256(string textoAEncriptar)
        {
            // Create a SHA256   
            using SHA256 sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(textoAEncriptar));
            // Convert byte array to a string   
            StringBuilder hashObtenido = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                hashObtenido.Append(bytes[i].ToString("x2"));
            }
            return hashObtenido.ToString();
        }
    }
}

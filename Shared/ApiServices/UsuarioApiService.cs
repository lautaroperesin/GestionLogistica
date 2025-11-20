using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GestionLogisticaBackend.DTOs.Conductor;
using GestionLogisticaBackend.DTOs.Usuario;
using Microsoft.Extensions.Caching.Memory;
using Service.Interfaces;
using Service.Services;
using Shared.Interfaces;
using Sprache;

namespace Shared.ApiServices
{
    public class UsuarioApiService : GenericApiService<UsuarioDto>, IUsuarioService
    {
        public UsuarioApiService(HttpClient? httpClient = null, IMemoryCache? memoryCache = null) : base(httpClient, memoryCache)
        {
        }

        public async Task<UsuarioDto?> GetUserByEmailAsync(string email)
        {
            SetAuthorizationHeader();
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/byemail?email={email}");
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al obtener el usuario por email: {response.StatusCode} - {content}");
                }
                return JsonSerializer.Deserialize<UsuarioDto>(content, _options);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en UsuarioService al obtener por email: {ex.Message}");
            }
        }

        // add usuario
        public async Task<UsuarioDto> AddAsync(CreateUsuarioDto createUsuarioDto)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync(_endpoint, createUsuarioDto);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al agregar el usuario: {response.StatusCode} - {content}");
            }

            return JsonSerializer.Deserialize<UsuarioDto>(content, _options)!;
        }

        public async Task<bool> LoginInSystem(string email, string password)
        {
            var usuarioDto = new UsuarioDto
            {
                Email = email,
                Password = password
            };
            var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/login", usuarioDto);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al iniciar sesión: {response.StatusCode}");
            }
            var result = JsonSerializer.Deserialize<bool>(content, _options);
            return result;
        }
    }
}

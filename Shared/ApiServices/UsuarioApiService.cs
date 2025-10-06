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
using Service.Interfaces;
using Service.Services;
using Shared.Interfaces;
using Sprache;

namespace Shared.ApiServices
{
    public class UsuarioApiService : GenericApiService<UsuarioDto>, IUsuarioService
    {
        public UsuarioApiService(HttpClient? httpClient = null) : base(httpClient)
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
    }
}

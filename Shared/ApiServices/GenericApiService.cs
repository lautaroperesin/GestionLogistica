using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Service.Interfaces;
using Shared.Utils;

namespace Service.Services
{
    public class GenericApiService<T> : IGenericApiService<T> where T : class
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _endpoint;
        protected readonly JsonSerializerOptions _options;
        public static string? token;

        public GenericApiService(HttpClient? httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var typeName = typeof(T).Name;
            if (typeName.EndsWith("Dto", StringComparison.OrdinalIgnoreCase))
            {
                typeName = typeName.Substring(0, typeName.Length - 3);
            }

            _endpoint = Shared.Properties.Resources.urlApi + ApiEndpoints.GetEndpoint(typeName);
        }

        protected void SetAuthorizationHeader()
        {
            // Usar el store centralizado para obtener el token, esto evita inconsistencias
            var currentToken = AuthTokenStore.Token ?? token;

            if (!string.IsNullOrEmpty(currentToken))
            {
                if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {currentToken}");
            }
            else
            {
                throw new Exception("Token de autenticación no proporcionado.");
            }
        }

        public async Task<T?> AddAsync(T? entity)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync(_endpoint, entity);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al agregar el dato: {response.StatusCode} - {content}");
            }
            return JsonSerializer.Deserialize<T>(content, _options);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"{_endpoint}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al eliminar el dato: {response.StatusCode}");
            }
            return response.IsSuccessStatusCode;
        }

        public async Task<List<T>?> GetAllAsync(string? filtro = "")
        {
            SetAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_endpoint}?filtro={filtro}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<T>>(content, _options);
            }
            else
            {
                throw new Exception("Error al obtener los datos.");
            }
        }

        public async Task<List<T>?> GetAllDeletedsAsync()
        {
            SetAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_endpoint}/deleteds");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener los datos: {response.StatusCode}");
            }
            return JsonSerializer.Deserialize<List<T>>(content, _options);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_endpoint}/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener los datos: {response.StatusCode}");
            }
            return JsonSerializer.Deserialize<T>(content, _options);
        }

        public async Task<bool> RestoreAsync(int id)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PutAsync($"{_endpoint}/restore/{id}", null);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al restaurar el dato: {response.StatusCode}");
            }
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(T? entity)
        {
            SetAuthorizationHeader();
            var idValue = entity.GetType().GetProperty("Id").GetValue(entity);
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}/{idValue}", entity);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Hubo un problema al actualizar");
            }
            else
            {
                return response.IsSuccessStatusCode;
            }
        }
    }
}

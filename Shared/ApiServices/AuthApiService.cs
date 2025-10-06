using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GestionLogisticaBackend.DTOs.Usuario;
using Shared.Utils;

namespace Shared.ApiServices
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;
        public AuthApiService(HttpClient? httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
        }

        protected void SetAuthorizationHeader(HttpClient _httpClient)
        {
            if (!string.IsNullOrEmpty(AuthTokenStore.Token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthTokenStore.Token);
            else
                throw new ArgumentException("Token no definido.", nameof(AuthTokenStore.Token));
        }

        public async Task<string?> Login(UsuarioDto? login)
        {
            if (login == null)
            {
                throw new ArgumentException("El objeto login es nulo.");
            }

            try
            {
                var urlApi = Properties.Resources.urlApi;
                var endpointAuth = ApiEndpoints.GetEndpoint("Login");
                var response = await _httpClient.PostAsJsonAsync($"{urlApi}{endpointAuth}/login", login);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    AuthTokenStore.Token = result;
                    return null;
                }
                else
                {
                    // Retornar el mensaje de error desde la respuesta
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en AuthService al loguearse: {ex.Message}");
            }
        }

        public async Task<bool> ResetPassword(UsuarioDto? login)
        {
            if (login == null)
            {
                throw new ArgumentException("El objeto login no llego.");
            }
            try
            {
                var UrlApi = Properties.Resources.urlApi;
                var endpointAuth = ApiEndpoints.GetEndpoint("Login");
                var client = new HttpClient();
                SetAuthorizationHeader(client);
                var response = await client.PostAsJsonAsync($"{UrlApi}{endpointAuth}/resetpassword/", login);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al resetear" + ex.Message);
            }
        }

        public async Task<bool> CreateUserWithEmailAndPassword(string email, string password, string nombre)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("Email, password o nombre no pueden ser nulos o vacíos.");
            }
            try
            {
                var UrlApi = Properties.Resources.urlApi;
                var endpointAuth = ApiEndpoints.GetEndpoint("Login");
                var client = new HttpClient();
                var newUser = new UsuarioDto
                {
                    Email = email,
                    Password = password,
                    //Nombre = nombre
                };
                var response = await client.PostAsJsonAsync($"{UrlApi}{endpointAuth}/register/", newUser);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    AuthTokenStore.Token = result;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear usuario" + ex.Message);
            }
        }
    }
}

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GestionLogisticaBackend.DTOs.Usuario;
using GestionLogisticaBackend.Services.Interfaces;
using Service.Services;
using Shared.Interfaces;
using Shared.Utils;

namespace Shared.ApiServices
{
    public class AuthApiService : IAuthService
    {
        public AuthApiService()
        {
        }

        public async Task<bool> CreateUserWithEmailAndPasswordAsync(string email, string password, string nombre)
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
                var newUser = new CreateUsuarioDto { Email = email, Password = password, Nombre = nombre };
                var response = await client.PostAsJsonAsync($"{UrlApi}{endpointAuth}/register/", newUser);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    // Guardar el resultado en AuthTokenStore
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

        //si no recibo el objeto IConfiguration en el constructor, creo un constructor vacio que instancie uno y lea el archivo appsettings.json


        public async Task<string?> Login(UsuarioDto? login)
        {
            if (login == null)
            {
                throw new ArgumentException("El objeto login no llego.");
            }
            try
            {
                var urlApi = Properties.Resources.urlApi;
                var endpointAuth = ApiEndpoints.GetEndpoint("Login");
                var client = new HttpClient();
                var response = await client.PostAsJsonAsync($"{urlApi}{endpointAuth}/login/", login);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    // Guardar el resultado en AuthTokenStore
                    AuthTokenStore.Token = result;
                    return null;
                }
                else
                {
                    //si no es exitoso, devuelvo el mensaje de error
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al loguearse->: " + ex.Message);
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
                var urlApi = Properties.Resources.urlApi;
                var endpointAuth = ApiEndpoints.GetEndpoint("Login");
                var client = new HttpClient();
                var response = await client.PostAsJsonAsync($"{urlApi}{endpointAuth}/resetpassword/", login);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al resetear el password->: " + ex.Message);
            }
        }
        public async Task<bool> DeleteUser(UsuarioDto? login)
        {
            if (login == null)
            {
                throw new ArgumentException("El objeto login no llego.");
            }
            try
            {
                var urlApi = Properties.Resources.urlApi;
                var endpointAuth = ApiEndpoints.GetEndpoint("Login");
                var client = new HttpClient();
                var response = await client.PostAsJsonAsync($"{urlApi}{endpointAuth}/deleteuser/", login);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el usuario en firebase->: " + ex.Message);
            }
        }
    }
}

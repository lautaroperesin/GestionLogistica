using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using GestionLogisticaBackend.DTOs.Vehiculo;
using GestionLogisticaBackend.DTOs.Pagination;
using Service.Services;
using GestionLogisticaBackend.Services.Interfaces;
using System.Net.Http.Json;

namespace Shared.ApiServices
{
    public class VehiculoApiService : GenericApiService<VehiculoDto>, IVehiculoService
    {
        public VehiculoApiService(HttpClient? httpClient = null) : base(httpClient)
        {
        }

        public async Task<IEnumerable<VehiculoDto>> GetVehiculosAsync()
        {
            SetAuthorizationHeader();
            var response = await _httpClient.GetAsync(_endpoint);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener vehículos: {response.StatusCode} - {content}");
            }

            var vehiculos = JsonSerializer.Deserialize<IEnumerable<VehiculoDto>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return vehiculos ?? new List<VehiculoDto>();
        }

        public async Task<VehiculoDto> CreateVehiculoAsync(CreateVehiculoDto vehiculoDto)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync(_endpoint, vehiculoDto);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al crear vehículo: {response.StatusCode} - {content}");
            }
            return JsonSerializer.Deserialize<VehiculoDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        public async Task<bool> DeleteVehiculoAsync(int id)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"{_endpoint}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar vehículo: {response.StatusCode} - {content}");
            }
            return true;
        }

        public async Task<VehiculoDto?> GetVehiculoByIdAsync(int id)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_endpoint}/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener vehículo: {response.StatusCode} - {content}");
            }
            return JsonSerializer.Deserialize<VehiculoDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public Task<IEnumerable<VehiculoDto>> GetVehiculosEliminadosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RestoreVehiculoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateVehiculoAsync(UpdateVehiculoDto vehiculoDto)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}/{vehiculoDto.IdVehiculo}", vehiculoDto);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar vehículo: {response.StatusCode} - {content}");
            }
            return true;
        }
    }
}

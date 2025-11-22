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
                throw new Exception($"Error al obtener conductores: {response.StatusCode} - {content}");
            }

            var vehiculos = JsonSerializer.Deserialize<IEnumerable<VehiculoDto>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return vehiculos ?? new List<VehiculoDto>();
        }

        public Task<VehiculoDto> CreateVehiculoAsync(CreateVehiculoDto clienteDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteVehiculoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<VehiculoDto?> GetVehiculoByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VehiculoDto>> GetVehiculosEliminadosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RestoreVehiculoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateVehiculoAsync(UpdateVehiculoDto clienteDto)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using GestionLogisticaBackend.DTOs.Conductor;
using GestionLogisticaBackend.DTOs.Pagination;
using Service.Services;
using GestionLogisticaBackend.Services.Interfaces;
using System.Net.Http.Json;

namespace Shared.ApiServices
{
    public class ConductorApiService : GenericApiService<ConductorDto>, IConductorService
    {
        public ConductorApiService(HttpClient? httpClient = null) : base(httpClient)
        {
        }

        public async Task<ConductorDto> CreateConductorAsync(CreateConductorDto conductorDto)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync(_endpoint, conductorDto);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al crear conductor: {response.StatusCode} - {content}");
            }
            return JsonSerializer.Deserialize<ConductorDto>(content, _options)!;
        }

        public async Task<bool> DeleteConductorAsync(int id)
        {
            return await DeleteAsync(id);
        }

        public async Task<ConductorDto?> GetConductorByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<PagedResult<ConductorDto>> GetConductoresAsync(PaginationParams pagParams)
        {
            SetAuthorizationHeader();
            var url = $"{_endpoint}?PageNumber={pagParams.PageNumber}&PageSize={pagParams.PageSize}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener conductores: {response.StatusCode} - {content}");
            }

            // Deserializar la respuesta
            var pagedResult = JsonSerializer.Deserialize<PagedResult<ConductorDto>>(content, _options);
            return pagedResult ?? new PagedResult<ConductorDto>();
        }

        public async Task<IEnumerable<ConductorDto>> GetConductoresConLicenciaVencidaAsync()
        {
            SetAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_endpoint}/licencias-vencidas");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener conductores con licencia vencida: {response.StatusCode} - {content}");
            }
            return JsonSerializer.Deserialize<List<ConductorDto>>(content, _options) ?? new List<ConductorDto>();
        }

        public async Task<bool> UpdateConductorAsync(UpdateConductorDto conductorDto)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}/{conductorDto.IdConductor}", conductorDto);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar conductor: {response.StatusCode} - {content}");
            }
            return response.IsSuccessStatusCode;
        }
    }
}

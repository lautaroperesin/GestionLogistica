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

namespace Shared.ApiServices
{
    public class ConductorApiService : GenericApiService<ConductorDto>, IConductorService
    {
        public ConductorApiService(HttpClient? httpClient = null) : base(httpClient)
        {
        }

        public Task<ConductorDto> CreateConductorAsync(CreateConductorDto conductorDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteConductorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ConductorDto?> GetConductorByIdAsync(int id)
        {
            throw new NotImplementedException();
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
            var pagedResult = JsonSerializer.Deserialize<PagedResult<ConductorDto>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return pagedResult ?? new PagedResult<ConductorDto>();
        }

        public Task<IEnumerable<ConductorDto>> GetConductoresConLicenciaVencidaAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateConductorAsync(UpdateConductorDto conductorDto)
        {
            throw new NotImplementedException();
        }
    }
}

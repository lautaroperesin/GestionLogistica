using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using GestionLogisticaBackend.DTOs.Conductor;
using GestionLogisticaBackend.DTOs.Pagination;
using Service.Services;

namespace Shared.ApiServices
{
    public class ConductorApiService : GenericApiService<ConductorDto>
    {
        public ConductorApiService(HttpClient? httpClient = null) : base(httpClient)
        {
        }

        public async Task<List<ConductorDto>> GetAllConductoresAsync(int pageNumber = 1, int pageSize = 1000)
        {
            SetAuthorizationHeader();
            var url = $"{_endpoint}?PageNumber={pageNumber}&PageSize={pageSize}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener conductores: {response.StatusCode} - {content}");
            }

            var result = JsonSerializer.Deserialize<PagedResult<ConductorDto>>(content, _options);
            return result?.Items?.ToList() ?? new List<ConductorDto>();
        }
    }
}

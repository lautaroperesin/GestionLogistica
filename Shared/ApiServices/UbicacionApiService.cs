using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using GestionLogisticaBackend.DTOs.Ubicacion;
using GestionLogisticaBackend.DTOs.Pagination;
using Service.Services;
using GestionLogisticaBackend.Services.Interfaces;
using System.Net.Http.Json;

namespace Shared.ApiServices
{
    public class UbicacionApiService : GenericApiService<UbicacionDto>, IUbicacionService, IPaisService, IProvinciaService, ILocalidadService
    {
        public UbicacionApiService(HttpClient? httpClient = null) : base(httpClient)
        {
        }

        public async Task<PagedResult<UbicacionDto>> GetUbicacionesAsync(PaginationParams pagParams)
        {
            SetAuthorizationHeader();
            var url = $"{_endpoint}?PageNumber={pagParams.PageNumber}&PageSize={pagParams.PageSize}";
            if (!string.IsNullOrEmpty(pagParams.SearchTerm))
            {
                url += $"&searchTerm={Uri.EscapeDataString(pagParams.SearchTerm)}";
            }
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener ubicaciones: {response.StatusCode} - {content}");
            }

            // Deserializar la respuesta en PagedResult<UbicacionDto>
            var pagedResult = JsonSerializer.Deserialize<PagedResult<UbicacionDto>>(content, _options);
            return pagedResult!;
        }

        public async Task<UbicacionDto> CreateUbicacionAsync(CreateUbicacionDto clienteDto)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync(_endpoint, clienteDto);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al crear ubicación: {response.StatusCode} - {content}");
            }
            return JsonSerializer.Deserialize<UbicacionDto>(content, _options)!;
        }

        public async Task<bool> DeleteUbicacionAsync(int id)
        {
            return await DeleteAsync(id);
        }

        public async Task<UbicacionDto?> GetUbicacionByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<UbicacionDto>> GetUbicacionsEliminadosAsync()
        {
            var result = await GetAllDeletedsAsync();
            return result ?? new List<UbicacionDto>();
        }

        public async Task<bool> RestoreUbicacionAsync(int id)
        {
            return await RestoreAsync(id);
        }

        public async Task<bool> UpdateUbicacionAsync(UpdateUbicacionDto clienteDto)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}/{clienteDto.IdUbicacion}", clienteDto);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar ubicación: {response.StatusCode} - {content}");
            }
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<LocalidadDto>> GetLocalidadesByProvinciaAsync(int provinciaId)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_endpoint}/provincias/{provinciaId}/localidades");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener localidades: {response.StatusCode} - {content}");
            }
            return JsonSerializer.Deserialize<List<LocalidadDto>>(content, _options) ?? new List<LocalidadDto>();
        }

        public async Task<IEnumerable<ProvinciaDto>> GetProvinciasByPaisAsync(int paisId)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_endpoint}/paises/{paisId}/provincias");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener provincias: {response.StatusCode} - {content}");
            }
            return JsonSerializer.Deserialize<List<ProvinciaDto>>(content, _options) ?? new List<ProvinciaDto>();
        }

        public async Task<List<PaisDto>> GetPaisesAsync()
        {
            SetAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_endpoint}/paises");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener paises: {response.StatusCode} - {content}");
            }
            return JsonSerializer.Deserialize<List<PaisDto>>(content, _options) ?? new List<PaisDto>();
        }
    }
}

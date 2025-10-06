using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GestionLogisticaBackend.DTOs.Envio;
using GestionLogisticaBackend.DTOs.Filters;
using GestionLogisticaBackend.DTOs.Pagination;
using GestionLogisticaBackend.Enums;
using GestionLogisticaBackend.Services.Interfaces;
using Service.Services;

namespace Shared.ApiServices
{
    public class EnvioApiService : GenericApiService<EnvioDto>, IEnvioService
    {
        public EnvioApiService(HttpClient? httpClient = null) : base(httpClient)
        {
        }

        public async Task<PagedResult<EnvioDto>> GetEnviosAsync(EnvioFilterDto filtros)
        {
            SetAuthorizationHeader();

            var queryParams = new List<string>();
            if (filtros == null) filtros = new EnvioFilterDto();

            if (filtros.IdConductor.HasValue) queryParams.Add($"IdConductor={Uri.EscapeDataString(filtros.IdConductor.Value.ToString())}");
            if (filtros.IdCliente.HasValue) queryParams.Add($"IdCliente={Uri.EscapeDataString(filtros.IdCliente.Value.ToString())}");
            if (filtros.IdVehiculo.HasValue) queryParams.Add($"IdVehiculo={Uri.EscapeDataString(filtros.IdVehiculo.Value.ToString())}");
            if (filtros.FechaSalidaDesde.HasValue) queryParams.Add($"FechaSalidaDesde={Uri.EscapeDataString(filtros.FechaSalidaDesde.Value.ToString("o"))}");
            if (filtros.FechaSalidaHasta.HasValue) queryParams.Add($"FechaSalidaHasta={Uri.EscapeDataString(filtros.FechaSalidaHasta.Value.ToString("o"))}");
            if (filtros.EstadoEnvio.HasValue) queryParams.Add($"EstadoEnvio={(int)filtros.EstadoEnvio.Value}");
            if (!string.IsNullOrWhiteSpace(filtros.NumeroSeguimiento)) queryParams.Add($"NumeroSeguimiento={Uri.EscapeDataString(filtros.NumeroSeguimiento)}");
            if (!string.IsNullOrWhiteSpace(filtros.Origen)) queryParams.Add($"Origen={Uri.EscapeDataString(filtros.Origen)}");
            if (!string.IsNullOrWhiteSpace(filtros.Destino)) queryParams.Add($"Destino={Uri.EscapeDataString(filtros.Destino)}");

            // Paginación
            queryParams.Add($"PageNumber={filtros.PageNumber}");
            queryParams.Add($"PageSize={filtros.PageSize}");

            var query = string.Join("&", queryParams);
            var url = string.IsNullOrWhiteSpace(query) ? _endpoint : $"{_endpoint}?{query}";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener envíos: {response.StatusCode} - {content}");
            }

            var result = JsonSerializer.Deserialize<PagedResult<EnvioDto>>(content, _options);
            return result ?? new PagedResult<EnvioDto> { Items = new List<EnvioDto>(), TotalItems = 0, PageNumber = filtros.PageNumber, PageSize = filtros.PageSize };
        }

        public async Task<EnvioDto?> GetEnvioByIdAsync(int id)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_endpoint}/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener envío: {response.StatusCode} - {content}");
            }
            return JsonSerializer.Deserialize<EnvioDto>(content, _options);
        }

        public async Task<EnvioDto> CreateEnvioAsync(CreateEnvioDto envioDto)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync(_endpoint, envioDto);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al crear envío: {response.StatusCode} - {content}");
            }
            return JsonSerializer.Deserialize<EnvioDto>(content, _options)!;
        }

        public async Task<EnvioDto?> UpdateEnvioAsync(int id, UpdateEnvioDto envioDto)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}/{id}", envioDto);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al actualizar envío: {response.StatusCode} - {content}");
            }
            return JsonSerializer.Deserialize<EnvioDto>(content, _options);
        }

        public async Task<bool> DeleteEnvioAsync(int id)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"{_endpoint}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al eliminar envío: {response.StatusCode}");
            }
            return response.IsSuccessStatusCode;
        }

        public async Task UpdateEnvioEstadoAsync(int id, EstadoEnvioEnum nuevoEstado)
        {
            SetAuthorizationHeader();
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_endpoint}/{id}/estado/")
            {
                Content = JsonContent.Create(nuevoEstado, options: _options)
            };

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar estado envío: {response.StatusCode} - {content}");
            }
        }
    }
}

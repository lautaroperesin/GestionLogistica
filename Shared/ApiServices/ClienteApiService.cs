using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using GestionLogisticaBackend.DTOs.Cliente;
using GestionLogisticaBackend.DTOs.Pagination;
using Service.Services;
using GestionLogisticaBackend.Services.Interfaces;
using System.Net.Http.Json;

namespace Shared.ApiServices
{
    public class ClienteApiService : GenericApiService<ClienteDto>, IClienteService
    {
        public ClienteApiService(HttpClient? httpClient = null) : base(httpClient)
        {
        }

        public async Task<PagedResult<ClienteDto>> GetClientesAsync(PaginationParams pagParams)
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
                throw new Exception($"Error al obtener clientes: {response.StatusCode} - {content}");
            }

            // Deserializar la respuesta en PagedResult<ClienteDto>
            var pagedResult = JsonSerializer.Deserialize<PagedResult<ClienteDto>>(content, _options);
            return pagedResult!;
        }

        public async Task<ClienteDto?> GetClienteByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<ClienteDto> CreateClienteAsync(CreateClienteDto clienteDto)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync(_endpoint, clienteDto);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al crear cliente: {response.StatusCode} - {content}");
            }
            return JsonSerializer.Deserialize<ClienteDto>(content, _options)!;
        }

        public async Task<bool> UpdateClienteAsync(UpdateClienteDto clienteDto)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}/{clienteDto.IdCliente}", clienteDto);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar cliente: {response.StatusCode} - {content}");
            }
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            return await DeleteAsync(id);
        }

        public async Task<bool> RestoreClienteAsync(int id)
        {
            return await RestoreAsync(id);
        }

        public async Task<IEnumerable<ClienteDto>> GetClientesEliminadosAsync()
        {
            var result = await GetAllDeletedsAsync();
            return result ?? new List<ClienteDto>();
        }
    }
}

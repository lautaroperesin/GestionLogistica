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
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener conductores: {response.StatusCode} - {content}");
            }

            // Deserializar la respuesta en PagedResult<ClienteDto>
            var pagedResult = JsonSerializer.Deserialize<PagedResult<ClienteDto>>(content, _options);
            return pagedResult!;
        }

        public Task<ClienteDto> CreateClienteAsync(CreateClienteDto clienteDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteClienteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ClienteDto?> GetClienteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClienteDto>> GetClientesEliminadosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RestoreClienteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateClienteAsync(UpdateClienteDto clienteDto)
        {
            throw new NotImplementedException();
        }
    }
}

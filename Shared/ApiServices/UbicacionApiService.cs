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

namespace Shared.ApiServices
{
    public class UbicacionApiService : GenericApiService<UbicacionDto>, IUbicacionService
    {
        public UbicacionApiService(HttpClient? httpClient = null) : base(httpClient)
        {
        }

        public async Task<PagedResult<UbicacionDto>> GetUbicacionesAsync(PaginationParams pagParams)
        {
            SetAuthorizationHeader();
            var url = $"{_endpoint}?PageNumber={pagParams.PageNumber}&PageSize={pagParams.PageSize}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener conductores: {response.StatusCode} - {content}");
            }

            // Deserializar la respuesta en PagedResult<UbicacionDto>
            var pagedResult = JsonSerializer.Deserialize<PagedResult<UbicacionDto>>(content, _options);
            return pagedResult!;
        }

        public Task<UbicacionDto> CreateUbicacionAsync(CreateUbicacionDto clienteDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUbicacionAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UbicacionDto?> GetUbicacionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UbicacionDto>> GetUbicacionsEliminadosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RestoreUbicacionAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUbicacionAsync(UpdateUbicacionDto clienteDto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LocalidadDto>> GetLocalidadesByProvinciaAsync(int provinciaId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProvinciaDto>> GetProvinciasByPaisAsync(int paisId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaisDto>> GetPaisesAsync()
        {
            throw new NotImplementedException();
        }
    }
}

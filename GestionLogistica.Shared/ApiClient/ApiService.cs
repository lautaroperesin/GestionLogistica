using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GestionLogistica.Shared.DTOs;

public class ApiService
{
    private readonly HttpClient _httpClient;
    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ConductorDto>> GetConductoresAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ConductorDto>>("api/Conductores");
    }
}
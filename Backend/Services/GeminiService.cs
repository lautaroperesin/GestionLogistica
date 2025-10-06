using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Service.Interfaces;

namespace GestionLogisticaBackend.Implementations
{
    public class GeminiService : IGeminiService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient = new HttpClient();

        public GeminiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string?> GetPromptResponse(string textPrompt)
        {
            if (string.IsNullOrWhiteSpace(textPrompt))
            {
                throw new ArgumentException("El prompt no puede estar vacío.");
            }

            try
            {
                var apiKey = _configuration["ApiKeyGemini"];
                var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";
                var payload = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = textPrompt }
                            }
                        }
                    },
                };
                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(result);
                var texto = doc.RootElement
                   .GetProperty("candidates")[0]
                   .GetProperty("content")
                   .GetProperty("parts")[0]
                   .GetProperty("text")
                   .GetString();
                return texto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio Gemini: {ex.Message}");
            }
        }
    }
}

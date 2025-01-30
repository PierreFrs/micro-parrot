using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using MicroParrot.AI.Core.Services;
using MicroParrot.AI.Core.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace MicroParrot.AI.Infrastructure.LLM;

public class OllamaService : ILlamaService
{
    private readonly OllamaConfig _config;
    private readonly ILogger<OllamaService> _logger;
    private readonly HttpClient _httpClient;

    public OllamaService(IOptions<OllamaConfig> config, ILogger<OllamaService> logger, HttpClient httpClient)
    {
        _config = config.Value;
        _logger = logger;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_config.BaseUrl);
    }

    public async Task<ChatResponse> GenerateResponseAsync(ChatRequest request)
    {
        try
        {
            var ollamaRequest = new
            {
                model = _config.ModelName,
                prompt = request.Message,
                temperature = _config.Temperature,
                stream = false
            };

            _logger.LogInformation("Sending request to Ollama: {Request}", JsonSerializer.Serialize(ollamaRequest));

            var content = new StringContent(
                JsonSerializer.Serialize(ollamaRequest),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("/api/generate", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("Received response from Ollama: {Response}", responseBody);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Ollama returned non-success status code: {StatusCode}", response.StatusCode);
                return new ChatResponse
                {
                    Success = false,
                    Error = $"Ollama returned status code {response.StatusCode}"
                };
            }

            var ollamaResponse = JsonSerializer.Deserialize<OllamaResponse>(responseBody);

            if (ollamaResponse == null || string.IsNullOrEmpty(ollamaResponse.Response))
            {
                _logger.LogError("Ollama returned null or empty response");
                return new ChatResponse
                {
                    Success = false,
                    Error = "Empty response from Ollama"
                };
            }

            return new ChatResponse
            {
                Message = ollamaResponse.Response,
                Success = true,
                Metadata = new Dictionary<string, object>
                {
                    { "model", _config.ModelName },
                    { "temperature", _config.Temperature }
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating response from Ollama");
            return new ChatResponse
            {
                Success = false,
                Error = $"Failed to generate response: {ex.Message}"
            };
        }
    }

    private class OllamaResponse
    {
        [JsonPropertyName("response")]
        public string Response { get; set; } = string.Empty;
    }
}
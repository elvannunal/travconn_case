using System.Net.Http.Json;
using Hobbies.Application.DTOs;
using Hobbies.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hobbies.Infrastructure.Services;

public class LogApiClient:ILogApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<LogApiClient> _logger;

    public LogApiClient(HttpClient httpClient, ILogger<LogApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task SendLogAsync(CreateLogDto createLogDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Logs", createLogDto);
        if (!response.IsSuccessStatusCode)
        {
          _logger.LogError($"Log API'ye kayıt gönderilemedi. Status: {response.StatusCode}");
        }
    }
}
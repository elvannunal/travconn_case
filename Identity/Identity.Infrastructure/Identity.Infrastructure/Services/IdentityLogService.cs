using System.Text;
using System.Text.Json;
using Identity.Application.DTOs;
using Identity.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Services;

public class IdentityLogService : IIdentityLogService
    {
    private readonly HttpClient _httpClient;
    private readonly ILogger<IdentityLogService> _logger;
    private readonly string _logApiUrl;

    public IdentityLogService(
        HttpClient httpClient, 
        ILogger<IdentityLogService> logger,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _logApiUrl = configuration["LogsApi:BaseUrl"] ?? "http://localhost:7003/api/logs";
    }

    public async Task LogTokenRequestAsync(LogTokenRequestDto logDto)
    {
        try
        {
            var jsonContent = JsonSerializer.Serialize(logDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_logApiUrl, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning(
                    "Token log could not be saved to Logs DB. Status: {StatusCode}", 
                    response.StatusCode);
            }
            else
            {
                _logger.LogInformation(
                    "Token request logged successfully for UserId: {UserId}", 
                    logDto.UserId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex, 
                "Failed to send token log to Logs API. UserId: {UserId}", 
                logDto.UserId);
        }
    }
}
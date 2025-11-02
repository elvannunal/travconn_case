using System.Text.Json;
using Hobbies.Application.Interfaces;
using Hobbies.Domain.Entites;
using Hobbies.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Hobbies.Infrastructure.Services;

public class LogService : ILogService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<LogService> _logger;

    public LogService(ApplicationDbContext context,ILogger<LogService> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task LogAsync(string operation, string entityType, object data, string userId)
    {
        try
        {
            var log = new Log
            {
                Operation = operation,
                EntityType = entityType,
                EntityData = JsonSerializer.Serialize(data),
                UserId = userId,
                TimeStamp = DateTime.UtcNow
            };

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation($"{operation}-{entityType}-User:{userId}");
        }
        catch (Exception e)
        {
            _logger.LogError($"Log kaydedilemedi:{e.Message}");
        }
    }
}
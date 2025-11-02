using System.Text.Json;
using Logs.Application.DTOs;
using Logs.Application.Interfaces;
using Logs.Domain.Entities;
using Logs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Logs.Infrastructure.Services;

public class LogService: ILogService
{
    private readonly LogsDbContext _context; 
    private readonly ILogger<LogService> _logger;

    public LogService(LogsDbContext context, ILogger<LogService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddLogAsync(CreateLogDto createDto)
    {
        try
        {
            var log = new Log
            {
                Operation = createDto.Operation,
                EntityType = createDto.EntityType,
                EntityData = JsonSerializer.Serialize(createDto.Data),
                UserId = createDto.UserId,
                TimeStamp = DateTime.UtcNow
            };

            _context.Logs.Add(log);
            await _context.SaveChangesAsync(); 

            _logger.LogInformation(
                "{Operation} - {EntityType} - User: {UserId}", 
                log.Operation, log.EntityType, log.UserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Log kaydedilemedi");
            throw;
        }
    }

    public async Task<IEnumerable<Log>> GetLogsAsync(LogFilterDto filter)
    {
        try
        {
            var query = _context.Logs.AsNoTracking().AsQueryable();

            if (filter.StartDate.HasValue)
                query = query.Where(l => l.TimeStamp >= filter.StartDate.Value);


            if (filter.EndDate.HasValue)
                query = query.Where(l => l.TimeStamp <= filter.EndDate.Value);

            if (!string.IsNullOrEmpty(filter.Operation))
                query = query.Where(l => l.Operation == filter.Operation);

            if (!string.IsNullOrEmpty(filter.EntityType))
                query = query.Where(l => l.EntityType == filter.EntityType);

            if (!string.IsNullOrEmpty(filter.UserId))
                query = query.Where(l => l.UserId == filter.UserId);

            return await query
                .OrderByDescending(l => l.TimeStamp)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Loglar alınamadı");
            return Enumerable.Empty<Log>();
        }
    }
}
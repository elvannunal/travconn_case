using Logs.Application.DTOs;
using Logs.Domain.Entities;

namespace Logs.Application.Interfaces;

public interface ILogService
{
    Task AddLogAsync(CreateLogDto createDto);
    Task<IEnumerable<Log>> GetLogsAsync(LogFilterDto filter);
}
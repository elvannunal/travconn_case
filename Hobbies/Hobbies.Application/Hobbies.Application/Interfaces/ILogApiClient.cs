using Hobbies.Application.DTOs;

namespace Hobbies.Application.Interfaces;

public interface ILogApiClient
{
    Task SendLogAsync(CreateLogDto createLogDto);
}
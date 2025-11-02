namespace Hobbies.Application.Interfaces;

public interface ILogService
{
  Task LogAsync(string opetaion, string entityType, object data, string userId);
}
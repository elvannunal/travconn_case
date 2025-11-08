namespace Hobbies.Application.Interfaces;

public interface ICurrentUserService
{
     string UserId { get; }
     string UserIpAddress { get; }
}
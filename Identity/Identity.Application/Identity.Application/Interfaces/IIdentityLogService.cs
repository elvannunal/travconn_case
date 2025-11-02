using Identity.Application.DTOs;

namespace Identity.Application.Interfaces;

public interface IIdentityLogService
{
    Task LogTokenRequestAsync(LogTokenRequestDto logDto);
}
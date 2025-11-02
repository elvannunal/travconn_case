
using Identity.Domain.Entities;

namespace Identity.Application.Interfaces;

public interface ITokenService
{
    Task<string> CreateTokenAsync(Account account);
}
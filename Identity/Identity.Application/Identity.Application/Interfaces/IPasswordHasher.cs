using Identity.Domain.Entities;

namespace Identity.Application.Interfaces;

public interface IPasswordHasher
{
    bool VerifyPassword(Account account, string password);

    string HashPassword(Account account, string password);
}
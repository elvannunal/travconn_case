using Identity.Application.Interfaces; 
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Services;

public class PasswordHasherService : IPasswordHasher 
{
    private readonly PasswordHasher<Account> _passwordHasher = new();

    public string HashPassword(Account account, string password)
    {
        return _passwordHasher.HashPassword(account, password);
    }

    public bool VerifyPassword(Account account, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, password);
        return result == PasswordVerificationResult.Success;
    }
}
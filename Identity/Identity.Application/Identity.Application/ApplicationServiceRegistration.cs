using Identity.Application.Interfaces;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        
        services.AddSingleton<IPasswordHasher<Account>, PasswordHasher<Account>>();
        
        services.AddScoped<IAuthService, AuthService>();
        
        return services;
    }   
}
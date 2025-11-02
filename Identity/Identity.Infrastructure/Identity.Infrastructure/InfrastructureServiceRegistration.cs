using Identity.Application.Interfaces;
using Identity.Infrastructure.Data; 
using Identity.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 

namespace Identity.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("IdentityConnection")));
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()!);
        services.AddScoped<IPasswordHasher, PasswordHasherService>(); 
        
        services.AddHttpClient<IIdentityLogService, IdentityLogService>(client =>
        {
            client.Timeout = TimeSpan.FromSeconds(5);
        });
        
        return services;
    }
}
using Hobbies.Application.Interfaces;
using Hobbies.Infrastructure.Data;
using Hobbies.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hobbies.Infrastructure;

public static class HobbiesServiceRegistration
{
    public static IServiceCollection AddHobbyInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("HobbiesConnection")));

        services.AddScoped<ILogService, LogService>();
        services.AddScoped<IHobbyService,HobbyService>();
        services.AddScoped<IUserHobbyService,UserHobbyService>();
        return services;
    }
}
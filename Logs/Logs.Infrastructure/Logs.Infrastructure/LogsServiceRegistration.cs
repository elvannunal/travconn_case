using Logs.Application.Interfaces;
using Logs.Infrastructure.Data;
using Logs.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logs.Infrastructure;

public static class LogsServiceRegistration
{
    public static IServiceCollection AddLogsServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LogsDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("LogsConnection")));

        services.AddScoped<ILogService, LogService>();
        
        return services;
    }
}
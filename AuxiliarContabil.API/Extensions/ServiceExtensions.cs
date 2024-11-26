using AuxiliarContabil.Domain.Interfaces.Repositories;
using AuxiliarContabil.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AuxiliarContabil.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SqlDbContext>(options =>
            options.UseSqlServer(Environment.GetEnvironmentVariable("MSSQL_CONNECTION_STRING")));
        return services;
    }
}
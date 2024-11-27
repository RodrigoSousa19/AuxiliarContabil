using AuxiliarContabil.Application.Services;
using AuxiliarContabil.Domain.Interfaces.Repositories;
using AuxiliarContabil.Domain.Interfaces.Services;
using AuxiliarContabil.Infrastructure.Context;
using AuxiliarContabil.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuxiliarContabil.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<SqlDbContext>(options =>
            options.UseSqlServer(Environment.GetEnvironmentVariable("MSSQL_CONNECTION_STRING")));
        return services;
    }

    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IComposicaoSalarialService, ComposicaoSalarialService>();
        services.AddScoped<IComposicaoSalarialRepository, ComposicaoSalarialRepository>();
        services.AddScoped<IDasService, DasService>();
        services.AddScoped<IExtratoBancarioService, ExtratoBancarioService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}

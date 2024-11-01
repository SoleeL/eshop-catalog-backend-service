using Catalog.Domain;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Extensions;

public static class InfrastructureExtension
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        IServiceCollection services = builder.Services;
        
        // Definir DBs context
        // Obtener variables de entorno
        var host = Environment.GetEnvironmentVariable("USER_PG_HOST");
        var database = Environment.GetEnvironmentVariable("USER_PG_DATABASE");
        var user = Environment.GetEnvironmentVariable("USER_PG_USER");
        var password = Environment.GetEnvironmentVariable("USER_PG_PASSWORD");

        var connectionString = $"Host={host};Database={database};Username={user};Password={password}";
        builder.Services.AddDbContext<CatalogDbContext>(optionsBuilder => optionsBuilder.UseNpgsql(connectionString));
        
        // Registrar Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IBrandRepository, BrandRepository>();
    }
}
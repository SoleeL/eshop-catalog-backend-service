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
        var host = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_HOST");
        var port = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_PORT") ?? "5432";
        var database = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_DB");
        var user = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_USER");
        var password = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_PASSWORD");

        var connectionString = $"Host={host};Port={port};Database={database};Username={user};Password={password}";
        builder.Services.AddDbContext<CatalogDbContext>(optionsBuilder => optionsBuilder.UseNpgsql(connectionString));
        
        // Registrar Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IBrandRepository, BrandRepository>();
    }
}
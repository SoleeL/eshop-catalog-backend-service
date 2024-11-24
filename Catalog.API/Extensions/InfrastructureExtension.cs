using Catalog.Domain;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Persistence.DbContexts;
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
        String? host = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_HOST");
        
        String? port = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_PORT");
        String? portReplica = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_REPLICA_PORT");
        
        String? database = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_DB");
        
        String? userRw = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_USER_READ_WRITE");
        String? passwordRw = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_USER_READ_WRITE_PASSWORD");
        
        String? userRo = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_USER_READ_ONLY");
        String? passwordRo = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_USER_READ_ONLY_PASSWORD");
        
        String primaryConnectionString = $"Host={host};Port={port};Database={database};Username={userRw};Password={passwordRw}";
        builder.Services.AddDbContext<CatalogPrimaryDbContext>(optionsBuilder => optionsBuilder.UseNpgsql(primaryConnectionString));
        
        String replicaConnectionString = $"Host={host};Port={portReplica};Database={database};Username={userRo};Password={passwordRo}";
        builder.Services.AddDbContext<CatalogReplicaDbContext>(optionsBuilder => optionsBuilder.UseNpgsql(replicaConnectionString));
        
        // Registrar Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IBrandRepository, BrandRepository>();
    }
}
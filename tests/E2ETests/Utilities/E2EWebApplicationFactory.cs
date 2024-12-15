using Catalog.Domain.Repositories;
using Catalog.Domain.Shared;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Persistence.DbContexts;
using Catalog.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace E2ETests.Utilities;

public class E2EWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Cargar variables de entorno:
        EnvironmentVariablesHelper.SetEnvironmentVariablesFromLaunchSettings();
        
        // AddInfrastructureServices
        builder.ConfigureServices(services =>
        {
            // Obtener variables de entorno
            string? host = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_HOST");
            string? port = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_PORT");
            string? portReplica = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_REPLICA_PORT");
            string? database = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_DB");
            string? userRw = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_USER_READ_WRITE");
            string? passwordRw = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_USER_READ_WRITE_PASSWORD");
            string? userRo = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_USER_READ_ONLY");
            string? passwordRo = Environment.GetEnvironmentVariable("ESHOP_CATALOG_DATABASE_USER_READ_ONLY_PASSWORD");

            // Configurar cadenas de conexión y contextos
            string primaryConnectionString = $"Host={host};Port={port};Database={database};Username={userRw};Password={passwordRw}";
            services.AddDbContext<CatalogPrimaryDbContext>(options => options.UseNpgsql(primaryConnectionString));

            string replicaConnectionString = $"Host={host};Port={portReplica};Database={database};Username={userRo};Password={passwordRo}";
            services.AddDbContext<CatalogReplicaDbContext>(options => options.UseNpgsql(replicaConnectionString));

            // Registrar servicios
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBrandStateRepository, BrandStateRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
        });
    }
}

public static class EnvironmentVariablesHelper
{
    public static void SetEnvironmentVariablesFromLaunchSettings(string profileName = "http")
    {
        // Ruta al archivo launchSettings.json
        string filePath = Path.Combine(
            Directory.GetCurrentDirectory(), 
            "..", 
            "..", 
            "..",
            "..",
            "..",
            "src", 
            "Catalog.API", 
            "Properties", 
            "launchSettings.json");

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"launchSettings.json no encontrado en la ruta: {filePath}");
        }

        // Leer y parsear el archivo JSON
        string json = File.ReadAllText(filePath);
        JObject launchSettings = JObject.Parse(json);

        // Obtener el perfil
        JToken? profile = launchSettings["profiles"]?[profileName];
        if (profile == null)
        {
            throw new KeyNotFoundException($"El perfil '{profileName}' no se encuentra en launchSettings.json");
        }

        // Obtener las variables de entorno
        JToken? environmentVariablesRaw = profile["environmentVariables"];
        if (environmentVariablesRaw == null)
        {
            throw new KeyNotFoundException($"No se encontraron variables de entorno en el perfil '{profileName}'.");
        }

        Dictionary<string, string> environmentVariables = environmentVariablesRaw?
            .ToObject<Dictionary<string, string>>() ?? new Dictionary<string, string>();

        // Establecer las variables de entorno
        foreach (var variable in environmentVariables)
        {
            Environment.SetEnvironmentVariable(variable.Key, variable.Value);
        }

        // Establecer el entorno de ASP.NET Core
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", environment);
    }
}
using Asp.Versioning;
using Microsoft.OpenApi.Models;

namespace Catalog.API.Extensions;

public static class ApiVersioningExtension
{
    public static void AddApiVersioning(this IHostApplicationBuilder builder)
    {
        IServiceCollection services = builder.Services;
        
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        });

        services.AddApiVersioning().AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            // Configuración opcional para agregar Swagger con soporte para versionado de APIs
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "API V1", Version = "v1" });
            options.SwaggerDoc("v2", new OpenApiInfo { Title = "API V2", Version = "v2" });
        });
    }
}
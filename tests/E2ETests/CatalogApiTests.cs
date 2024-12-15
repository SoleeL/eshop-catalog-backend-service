using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace E2ETests;

public class CatalogApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CatalogApiTests(WebApplicationFactory<Program> factory)
    {
        // Personalizar la configuración del factory
        WebApplicationFactory<Program> factoryBuilder = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Development"); // Usa el entorno "Development".

            builder.ConfigureServices(services =>
            {
                // Si necesitas asegurarte de que se usen tus variables de entorno.
                services.Configure<HostOptions>(options =>
                {
                    /* Configuración adicional si es necesario */
                });
            });
        });

        _client = factoryBuilder.CreateClient(); // Crear el cliente HTTP
    }

    [Fact]
    public async Task GetProducts_ReturnsOkResponse()
    {
        // Act
        var response = await _client.GetAsync("/api/brand");

        // Assert
        response.EnsureSuccessStatusCode();
    }
}
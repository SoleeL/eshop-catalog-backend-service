using Microsoft.AspNetCore.Mvc.Testing;

namespace E2ETests;

public class CatalogApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CatalogApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
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
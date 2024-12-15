using E2ETests.Utilities;

namespace E2ETests;

public class CatalogBrandAPITests : IClassFixture<E2EWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CatalogBrandAPITests(E2EWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProducts_ReturnsOkResponse()
    {
        var response = await _client.GetAsync("/api/brand");
        response.EnsureSuccessStatusCode();
    }
}
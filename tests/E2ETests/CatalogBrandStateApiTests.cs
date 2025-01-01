using System.Text;
using Catalog.Application.Commands.BrandStates;
using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using E2ETests.Utilities;
using Newtonsoft.Json;

namespace E2ETests;

public class CatalogBrandStateApiTests : IClassFixture<E2EWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    
    public CatalogBrandStateApiTests(E2EWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateCatalogBrandState_SuccessStatusCode()
    {
        CreateBrandStateCommand createBrandStateCommand = new CreateBrandStateCommand();
        createBrandStateCommand.Name = "Descartado";
        createBrandStateCommand.Description = "La marca no es valida para el catalog";
        
        StringContent content = new StringContent(JsonConvert.SerializeObject(createBrandStateCommand), UTF8Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync("/api/brandstatus", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task GetCatalogBrandState_SuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/brandstatus");
        response.EnsureSuccessStatusCode();
    }
}
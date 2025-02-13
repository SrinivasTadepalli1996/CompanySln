using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;
using CompanyApi;
using CompanyApi.Models;

public class CompanyControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CompanyControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    private StringContent GetJsonContent(object obj) =>
        new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");

    [Fact]
    public async Task GetCompanies_ShouldReturnSuccess()
    {
        var response = await _client.GetAsync("/api/Company");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task AddCompany_ShouldReturnCreated()
    {
        var newCompany = new { Name = "Test Co", Exchange = "NASDAQ", Ticker = "TST", Isin = "US12345678" };
        var content = GetJsonContent(newCompany);

        var response = await _client.PostAsync("/api/Company", content);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }



    [Fact]
    public async Task UpdateCompany_ShouldReturnNoContent()
    {
        var newCompany = new { Name = "Old Co", Exchange = "NASDAQ", Ticker = "OLD", Isin = "US98765432" };
        var createResponse = await _client.PostAsync("/api/Company", GetJsonContent(newCompany));
        var createdCompany = JsonSerializer.Deserialize<Company>(
            await createResponse.Content.ReadAsStringAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        var updatedCompany = new { Id = createdCompany.Id, Name = "Updated Co", Exchange = "NASDAQ", Ticker = "UPD", Isin = "US87654321" };
        var updateResponse = await _client.PutAsync($"/api/Company/{createdCompany.Id}", GetJsonContent(updatedCompany));

        updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteCompany_ShouldReturnNoContent()
    {
        var newCompany = new { Name = "DeleteMe", Exchange = "NYSE", Ticker = "DEL", Isin = "US99887766" };
        var createResponse = await _client.PostAsync("/api/Company", GetJsonContent(newCompany));
        var createdCompany = JsonSerializer.Deserialize<Company>(
            await createResponse.Content.ReadAsStringAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        var deleteResponse = await _client.DeleteAsync($"/api/Company/{createdCompany.Id}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}

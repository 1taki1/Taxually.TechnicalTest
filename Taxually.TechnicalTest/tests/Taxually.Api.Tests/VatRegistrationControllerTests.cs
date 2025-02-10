using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Taxually.Api.Models.Requests;

namespace Taxually.Api.Tests;

public class VatRegistrationControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    readonly HttpClient _client;

    public VatRegistrationControllerTests(WebApplicationFactory<Program> application)
    {
        _client = application.CreateClient();
    }

    [Fact]
    public async Task Post_WithValidRequest_ShouldReturnOk()
    {
        var response = await _client.PostAsJsonAsync("api/VatRegistration",
            new VatRegistrationRequest() { CompanyName = "test", CompanyId = "test", Country = Countries.GB });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task Post_WithMissingCompanyIdRequest_ShouldReturnBadRequest()
    {
        var response = await _client.PostAsJsonAsync("api/VatRegistration",
            new VatRegistrationRequest() { CompanyName = "test", CompanyId = "", Country = Countries.GB });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task Post_WithMissingCompanyNameRequest_ShouldReturnBadRequest()
    {
        var response = await _client.PostAsJsonAsync("api/VatRegistration",
            new VatRegistrationRequest() { CompanyName = "", CompanyId = "test", Country = Countries.GB });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task Post_WithMissingCountryRequest_ShouldReturnBadRequest()
    {
        var response = await _client.PostAsJsonAsync("api/VatRegistration",
            new VatRegistrationRequest() { CompanyName = "", CompanyId = "test" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
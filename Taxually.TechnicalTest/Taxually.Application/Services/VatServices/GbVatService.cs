using Microsoft.Extensions.Options;
using Taxually.Application.Dtos;
using Taxually.Application.Interfaces;
using Taxually.Application.Options;

namespace Taxually.Application.Services.VatServices;

public class GbVatService : IVatService
{
    private readonly ITaxuallyHttpClient _httpClient;
    private readonly GbVatServiceOptions _options;

    public GbVatService(ITaxuallyHttpClient httpClient, IOptions<GbVatServiceOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }
    public async Task RegisterCompany(VatRegistrationModel request)
    {
        // UK has an API to register for a VAT number
        await _httpClient.PostAsync(_options.Url, request);
    }
}
using Microsoft.Extensions.Options;
using NSubstitute;
using Taxually.Application.Dtos;
using Taxually.Application.Interfaces;
using Taxually.Application.Options;
using Taxually.Application.Services.VatServices;

namespace Taxually.Application.Tests;

public class GbVatServiceTests
{
    [Fact]
    public async Task RegisterCompany()
    {
        var moqTaxuallyHttpClient = Substitute.For<ITaxuallyHttpClient>();
        var optionsMonitor = Substitute.For<IOptions<GbVatServiceOptions>>();
        optionsMonitor.Value.Returns(new GbVatServiceOptions(){Url = "test"});
        var service = new GbVatService(moqTaxuallyHttpClient, optionsMonitor);

        await service.RegisterCompany(new VatRegistrationModel("company", "id", VatCountriesEnum.GB));
        await moqTaxuallyHttpClient.Received(1).PostAsync(Arg.Any<string>(), Arg.Any<VatRegistrationModel>());
    }
}
using System.Text;
using Microsoft.Extensions.Options;
using NSubstitute;
using Taxually.Application.Dtos;
using Taxually.Application.Interfaces;
using Taxually.Application.Options;
using Taxually.Application.Services.VatServices;

namespace Taxually.Application.Tests;

public class FrVatServiceTests
{
    [Fact]
    public async Task RegisterCompany()
    {
        var moqTaxuallyQueueClient = Substitute.For<ITaxuallyQueueClient>();
        var optionsMonitor = Substitute.For<IOptions<FrVatServiceOptions>>();
        optionsMonitor.Value.Returns(new FrVatServiceOptions() { QueueName = "test" });
        var service = new FrVatService(moqTaxuallyQueueClient, optionsMonitor);

        var request = new VatRegistrationModel("company", "id", VatCountriesEnum.FR);

        await service.RegisterCompany(request);

        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("CompanyName,CompanyId");
        csvBuilder.AppendLine($"{request.CompanyName}{request.CompanyId}");
        var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());

        await moqTaxuallyQueueClient.Received(1).EnqueueAsync(
            Arg.Any<string>(),
            Arg.Is<byte[]>(x => x.SequenceEqual(csv)));
    }
}
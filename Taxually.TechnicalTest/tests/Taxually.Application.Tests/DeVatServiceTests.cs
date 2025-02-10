using System.Xml.Serialization;
using Microsoft.Extensions.Options;
using NSubstitute;
using Taxually.Application.Dtos;
using Taxually.Application.Interfaces;
using Taxually.Application.Options;
using Taxually.Application.Services.VatServices;

namespace Taxually.Application.Tests;

public class DeVatServiceTests
{
    [Fact]
    public async Task RegisterCompany()
    {
        var moqTaxuallyQueueClient = Substitute.For<ITaxuallyQueueClient>();
        var optionsMonitor = Substitute.For<IOptions<DeVatServiceOptions>>();
        optionsMonitor.Value.Returns(new DeVatServiceOptions(){QueueName = "test"});
        var service = new DeVatService(moqTaxuallyQueueClient, optionsMonitor);

        var request = new VatRegistrationModel("company", "id", VatCountriesEnum.DE);
        await service.RegisterCompany(request);
        
        using var stringWriter = new StringWriter();
        var serializer = new XmlSerializer(typeof(VatRegistrationModel));
        serializer.Serialize(stringWriter, request);
        var xml = stringWriter.ToString();
        
        await moqTaxuallyQueueClient.Received(1).EnqueueAsync(Arg.Any<string>(),  Arg.Is(xml));
    }
}
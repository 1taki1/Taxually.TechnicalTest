using System.Xml.Serialization;
using Microsoft.Extensions.Options;
using Taxually.Application.Dtos;
using Taxually.Application.Interfaces;
using Taxually.Application.Options;

namespace Taxually.Application.Services.VatServices;

public class DeVatService: IVatService
{
    private readonly ITaxuallyQueueClient _taxuallyQueueClient;
    private readonly DeVatServiceOptions _options;

    public DeVatService(ITaxuallyQueueClient taxuallyQueueClient, IOptions<DeVatServiceOptions> options)
    {
        _taxuallyQueueClient = taxuallyQueueClient;
        _options = options.Value;
    }
    public async Task RegisterCompany(VatRegistrationModel request)
    {
        using var stringWriter = new StringWriter();
        var serializer = new XmlSerializer(typeof(VatRegistrationModel));
        serializer.Serialize(stringWriter, request);
        var xml = stringWriter.ToString();
        // Queue xml doc to be processed
        await _taxuallyQueueClient.EnqueueAsync(_options.QueueName, xml);
    }
}
using System.Text;
using Microsoft.Extensions.Options;
using Taxually.Application.Dtos;
using Taxually.Application.Interfaces;
using Taxually.Application.Options;

namespace Taxually.Application.Services.VatServices;

public class FrVatService: IVatService
{
    private readonly ITaxuallyQueueClient _taxuallyQueueClient;
    private readonly FrVatServiceOptions _options;

    public FrVatService(ITaxuallyQueueClient taxuallyQueueClient, IOptions<FrVatServiceOptions> options)
    {
        _taxuallyQueueClient = taxuallyQueueClient;
        _options = options.Value;
    }

    public async Task RegisterCompany(VatRegistrationModel request)
    {
        // France requires an excel spreadsheet to be uploaded to register for a VAT number
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("CompanyName,CompanyId");
        csvBuilder.AppendLine($"{request.CompanyName}{request.CompanyId}");
        var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());
        // Queue file to be processed
        await _taxuallyQueueClient.EnqueueAsync(_options.QueueName, csv);
    }
}
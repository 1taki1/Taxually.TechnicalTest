using Taxually.Application.Dtos;
using Taxually.Application.Interfaces;

namespace Taxually.Application.Services.VatServices;

public class VatServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public VatServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IVatService GetVatService(VatCountriesEnum country)
    {
        switch (country)
        {
            case VatCountriesEnum.GB:
                return (IVatService)_serviceProvider.GetService(typeof(GbVatService))!;
            case VatCountriesEnum.FR:
                return (IVatService)_serviceProvider.GetService(typeof(FrVatService))!;
            case VatCountriesEnum.DE:
                return (IVatService)_serviceProvider.GetService(typeof(DeVatService))!;
            default:
                throw new InvalidOperationException($"Unknown country: {country}");
        }
    }
}
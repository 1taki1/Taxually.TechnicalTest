using NSubstitute;
using Taxually.Application.Dtos;
using Taxually.Application.Interfaces;
using Taxually.Application.Services.VatServices;

namespace Taxually.Application.Tests;

public class VatServiceFactoryTests
{
    [Fact]
    public void GetVatService_Should_WorkForAllEnums()
    {
        var moqServiceProvider = Substitute.For<IServiceProvider>();
        var moqVatService = Substitute.For<IVatService>();
        moqServiceProvider.GetService(Arg.Any<Type>()).Returns(moqVatService);
        var factory = new VatServiceFactory(moqServiceProvider);
        var countriesEnums = Enum.GetValues<VatCountriesEnum>();

        foreach (var country in countriesEnums)
        {
            factory.GetVatService(country);
        }
    }
    
    [Fact]
    public void GetVatService_ShouldReturnGb_WhenGb()
    {
        var moqServiceProvider = Substitute.For<IServiceProvider>();
        var moqVatService = Substitute.For<IVatService>();
        moqServiceProvider.GetService(Arg.Any<Type>()).Returns(moqVatService);
        var factory = new VatServiceFactory(moqServiceProvider);
        
        factory.GetVatService(VatCountriesEnum.GB);
        moqServiceProvider.Received(1).GetService(Arg.Is<Type>(x => x == typeof(GbVatService)));
    }
    
    [Fact]
    public void GetVatService_ShouldReturnFr_WhenGb()
    {
        var moqServiceProvider = Substitute.For<IServiceProvider>();
        var moqVatService = Substitute.For<IVatService>();
        moqServiceProvider.GetService(Arg.Any<Type>()).Returns(moqVatService);
        var factory = new VatServiceFactory(moqServiceProvider);
        
        factory.GetVatService(VatCountriesEnum.FR);
        moqServiceProvider.Received(1).GetService(Arg.Is<Type>(x => x == typeof(FrVatService)));
    }
    
    [Fact]
    public void GetVatService_ShouldReturnDe_WhenGb()
    {
        var moqServiceProvider = Substitute.For<IServiceProvider>();
        var moqVatService = Substitute.For<IVatService>();
        moqServiceProvider.GetService(Arg.Any<Type>()).Returns(moqVatService);
        var factory = new VatServiceFactory(moqServiceProvider);
        
        factory.GetVatService(VatCountriesEnum.DE);
        moqServiceProvider.Received(1).GetService(Arg.Is<Type>(x => x == typeof(DeVatService)));
    }
}
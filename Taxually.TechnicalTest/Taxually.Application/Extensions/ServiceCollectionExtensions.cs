using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taxually.Application.Interfaces;
using Taxually.Application.Options;
using Taxually.Application.Services.VatServices;

namespace Taxually.Application.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all Application layer services for DI.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<GbVatServiceOptions>(configuration.GetSection(nameof(GbVatServiceOptions)));
        services.Configure<FrVatServiceOptions>(configuration.GetSection(nameof(FrVatServiceOptions)));
        services.Configure<DeVatServiceOptions>(configuration.GetSection(nameof(DeVatServiceOptions)));
        
        services.AddTransient<VatServiceFactory>();
        services.AddTransient<GbVatService>()
            .AddTransient<IVatService, GbVatService>(s => s.GetRequiredService<GbVatService>());        
        services.AddTransient<FrVatService>()
            .AddTransient<IVatService, FrVatService>(s => s.GetRequiredService<FrVatService>());        
        services.AddTransient<DeVatService>()
            .AddTransient<IVatService, DeVatService>(s => s.GetRequiredService<DeVatService>());
        return services;
    }
}
using Microsoft.Extensions.DependencyInjection;
using Taxually.Application.Interfaces;

namespace Taxually.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all Infrastructure layer services for DI.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<ITaxuallyHttpClient, TaxuallyHttpClient>();
        services.AddScoped<ITaxuallyQueueClient, TaxuallyQueueClient>();
        return services;
    }
}
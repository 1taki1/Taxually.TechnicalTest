using FluentValidation;
using Taxually.Api.Models.Requests;
using Taxually.Api.Validators;

namespace Taxually.Api.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all Api layer services for DI.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApiDependencies(this IServiceCollection services)
    {
        services.AddScoped<IValidator<VatRegistrationRequest>, VatRegistrationRequestValidator>();
        return services;
    }
}
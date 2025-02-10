using FluentValidation;
using Taxually.Api.Models.Requests;

namespace Taxually.Api.Validators;

public class VatRegistrationRequestValidator:  AbstractValidator<VatRegistrationRequest>
{
    public VatRegistrationRequestValidator()
    {
        RuleFor(vatRegistrationRequest => vatRegistrationRequest.CompanyName).NotEmpty();
        RuleFor(vatRegistrationRequest => vatRegistrationRequest.CompanyId).NotEmpty();
        RuleFor(vatRegistrationRequest => vatRegistrationRequest.Country).NotEmpty();
    }
    
}
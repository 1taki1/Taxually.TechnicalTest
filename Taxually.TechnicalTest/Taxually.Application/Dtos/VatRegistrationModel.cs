namespace Taxually.Application.Dtos;

public readonly record struct VatRegistrationModel(string CompanyName, string CompanyId, VatCountriesEnum Country);
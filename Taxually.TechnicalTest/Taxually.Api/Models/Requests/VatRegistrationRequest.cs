using Taxually.Application.Dtos;

namespace Taxually.Api.Models.Requests;

public class VatRegistrationRequest
{
    public string CompanyName { get; set; }
    public string CompanyId { get; set; }
    public Countries Country { get; set; }


    public VatRegistrationModel CreateVatRegistrationModel()
    {
        var vatCountriesEnum = Country switch
        {
            Countries.GB => VatCountriesEnum.GB,
            Countries.FR => VatCountriesEnum.FR,
            Countries.DE => VatCountriesEnum.DE,
            _ => throw new ArgumentOutOfRangeException()
        };

        return new VatRegistrationModel(CompanyName, CompanyId, vatCountriesEnum);
    }
}
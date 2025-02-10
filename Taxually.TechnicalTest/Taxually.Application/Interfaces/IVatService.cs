using Taxually.Application.Dtos;

namespace Taxually.Application.Interfaces;

public interface IVatService
{
    Task RegisterCompany(VatRegistrationModel request);
}
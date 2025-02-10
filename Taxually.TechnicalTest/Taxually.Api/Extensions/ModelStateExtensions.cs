using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Taxually.Api.Extensions;

public static class ModelStateExtensions
{
    public static void AddValidationErrors(this ModelStateDictionary modelState, ValidationResult result)
    {
        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
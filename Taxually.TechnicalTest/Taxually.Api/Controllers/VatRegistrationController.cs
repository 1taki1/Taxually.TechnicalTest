using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Taxually.Api.Extensions;
using Taxually.Api.Models.Requests;
using Taxually.Application.Interfaces;
using Taxually.Application.Services.VatServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase
    {
        private readonly ILogger<VatRegistrationController> _logger;
        private readonly VatServiceFactory _factory;

        public VatRegistrationController(ILogger<VatRegistrationController> logger, VatServiceFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] VatRegistrationRequest request,
            IValidator<VatRegistrationRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                ModelState.AddValidationErrors(validationResult);
                return ValidationProblem(ModelState);
            }

            var applicationRequestModel = request.CreateVatRegistrationModel();

            IVatService vatService;
            try
            {
                vatService = _factory.GetVatService(applicationRequestModel.Country);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e, "VetService not registered, VatRegistrationController::Post");
                return Problem();
            }

            await vatService.RegisterCompany(applicationRequestModel);
            return Ok();
        }
    }
}
using CustomerApplication.Logic;
using CustomerApplication.Models.RequestModel;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApplication.WebApp.Controllers;

[ApiController]
[Route("api/customer-hours")]
public class CustomerHoursController : ControllerBase
{
    private readonly ICustomerHoursLogic _logic;
    private readonly ILogger<CustomerHoursController> _logger;
    public CustomerHoursController(ICustomerHoursLogic logic, ILogger<CustomerHoursController> logger)
    {
        _logic = logic;
        _logger = logger;
    }

    [HttpGet]
    [Route("/api/customer-hours/{customerId}")]
    public IActionResult GetCustomerOpenings(int customerId)
    {
        return Ok(_logic.GetCustomerHours(customerId));
    }

    [HttpPost]
    [Route("/api/customer-hours")]
    public IActionResult CreateCustomerOpening([FromBody] CustomerHoursRequest hours)
    {
        if (_logic.CreateCustomerHours(hours, out ValidationResult validation))
        {
            _logger.LogInformation("Customer hours added successfully: {@hours}", hours);
            return Ok(new ResponseMessageRequest("Customer hours added successfully"));
        }

        _logger.LogWarning("Customer hours creation validation failed; Halting Creation");
        _logger.LogWarning("Validation responses: {@errors}", validation.Errors);

        if (!validation.IsValid) return BadRequest(validation.Errors);
        return BadRequest(new ResponseMessageRequest("Customer opening creation failed. Please attempt the request again. If the issue persists please contact support."));
    }

    [HttpPut]
    public IActionResult UpdateCustomerOpening([FromBody] CustomerHoursRequest hours)
    {
        if (_logic.UpdateCustomerHours(hours, out ValidationResult validation))
        {
            _logger.LogInformation("Customer hours update validation succeeded; proceeding with update");
            return Ok(new ResponseMessageRequest("Customer hours updated successfully"));
        }

        _logger.LogWarning("Customer hours update validation failed; Halting update");
        _logger.LogWarning("Validation responses: {@errors}", validation.Errors);

        if (!validation.IsValid) return BadRequest(validation.Errors);
        return BadRequest(new ResponseMessageRequest("Customer opening update failed. Please attempt the request again. If the issue persists please contact support."));
    }

    [HttpDelete]
    [Route("/api/customer-hours/{customerOpeningId}")]
    public IActionResult DeleteCustomerOpening(int customerOpeningId)
    {
        if (_logic.DeleteCustomerHours(customerOpeningId))
        {
            _logger.LogInformation("Customer hours with id {id} were deleted", customerOpeningId);
            return Ok(new ResponseMessageRequest("Customer hours deleted from the system"));
        }

        _logger.LogWarning("Customer hours with id {id} could not be deleted", customerOpeningId);

        return BadRequest(new ResponseMessageRequest("Customer opening not be deleted. Please verify that the provided customer opening ID is correct"));
    }

    [HttpDelete]
    [Route("/api/customer-hours/{customerId}/all")]
    public IActionResult DeleteAllCustomerOpenings(int customerId)
    {
        if (_logic.DeleteAllCustomerHours(customerId))
        {
            _logger.LogInformation("Customer hours for customer {id} were deleted", customerId);
            return Ok(new ResponseMessageRequest("All customer hours deleted"));
        }

        _logger.LogWarning("Customer hours for customer {id} were not all deleted", customerId);

        return BadRequest(new ResponseMessageRequest("Failed to delete all customer openings. Please try the request again and if the issue persists contact support. "));
    }
}

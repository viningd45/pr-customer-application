using CustomerApplication.Logic;
using CustomerApplication.Models.RequestModel;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApplication.WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerLogic _customerLogic;
    private readonly ILogger<CustomersController> _logger;
    public CustomersController(ICustomerLogic customerLogic, ILogger<CustomersController> logger)
    {
        _customerLogic = customerLogic;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetCustomers([FromQuery] CustomerRequest filter)
    {
        return Ok(_customerLogic.GetCustomers(filter));
    }

    [HttpGet]
    [Route("/api/customers/{customerId}")]
    public IActionResult GetCustomer(int customerId)
    {
        return Ok(_customerLogic.GetCustomer(customerId));
    }

    [HttpPost]
    public IActionResult CreateCustomer([FromBody] CustomerRequest customer)
    {
        if(_customerLogic.CreateCustomer(customer, out ValidationResult validation))
        {
            _logger.LogInformation("Customer was created successfully");
            return Ok(new ResponseMessageRequest("New customer created successfully"));
        }

        _logger.LogWarning("Customer creation validation failed; Halting Creation");
        _logger.LogWarning("Validation responses: {@errors}", validation.Errors);

        if (!validation.IsValid) return BadRequest(validation.Errors);
        return BadRequest(new ResponseMessageRequest("Customer creation failed. Please attempt the request again. If the issue persists please contact support."));
    }

    [HttpPut]
    public IActionResult UpdateCustomer([FromBody] CustomerRequest customer)
    {
        if (_customerLogic.UpdateCustomer(customer, out ValidationResult validation))
        {
            _logger.LogInformation("Customer {id} was updated successfully", customer.Id);
            return Ok(new ResponseMessageRequest("Customer was updated successfully"));
        }

        _logger.LogWarning("Customer update validation failed; Halting update");
        _logger.LogWarning("Validation responses: {@errors}", validation.Errors);

        if (!validation.IsValid) return BadRequest(validation.Errors);
        return BadRequest(new ResponseMessageRequest("Customer update failed. Please attempt the request again. If the issue persists please contact support."));
    }

    [HttpDelete]
    [Route("/api/customers/{customerId}")]
    public IActionResult DeleteCustomer(int customerId)
    {
        if (_customerLogic.DeleteCustomer(customerId))
        {
            _logger.LogInformation("Customer {id} was deleted successfully", customerId);
            return Ok(new ResponseMessageRequest("Customer was deleted successfully"));
        }

        _logger.LogWarning("Customer {id} could not be deleted", customerId);

        return BadRequest("Customer could not be deleted. Please verify that the provided customer ID is correct");
    }
}

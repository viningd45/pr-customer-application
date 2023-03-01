using CustomerApplication.DataAccess;
using CustomerApplication.Logic.Validation;
using CustomerApplication.Models.DatabaseModel;
using CustomerApplication.Models.RequestModel;
using FluentValidation;
using FluentValidation.Results;

namespace CustomerApplication.Logic;

public class CustomerLogic : ICustomerLogic
{
    private readonly ICustomerData _data;
    private readonly IValidator<CustomerRequest> _validator;
    public CustomerLogic(ICustomerData data, IValidator<CustomerRequest> validator)
    {
        _data = data;
        _validator = validator;
    }

    public bool CreateCustomer(CustomerRequest customer, out ValidationResult validationResult)
    {
        validationResult = _validator.Validate(customer);

        if (!validationResult.IsValid) return false;
        return _data.CreateCustomer(new Customer(customer)) > 0;
    }

    public bool DeleteCustomer(int customerId)
    {
        return _data.DeleteCustomer(customerId) > 0;
    }

    public CustomerRequest GetCustomer(int id)
    {
        return new CustomerRequest(_data.GetCustomer(id));
    }

    public IEnumerable<CustomerRequest> GetCustomers(CustomerRequest filter)
    {
        return _data.GetCustomers(new Customer(filter)).Select(customer => new CustomerRequest(customer));
    }

    public bool UpdateCustomer(CustomerRequest customer, out ValidationResult validationResult)
    {
        validationResult = _validator.Validate(customer);

        if (!validationResult.IsValid) return false;
        return _data.UpdateCustomer(new Customer(customer)) > 0;
    }
}

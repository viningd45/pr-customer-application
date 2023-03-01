using CustomerApplication.Logic.Validation;
using CustomerApplication.Models.RequestModel;
using FluentValidation.TestHelper;

namespace CustomerApplication.Tests.Logic.Validation;

[TestFixture]
public class CustomerRequestValidatorTests
{
    private CustomerRequestValidator validator;

    [SetUp]
    public void Setup()
    {
        validator = new CustomerRequestValidator();
    }

    [Test]
    public void ShouldHaveError_When_PhoneNumberHasIncorrectLength()
    {
        CustomerRequest customerRequest= new();
        customerRequest.PhoneNumber = "3181231234";

        var result = validator.TestValidate(customerRequest);
        result.ShouldHaveValidationErrorFor(customer => customer.PhoneNumber);
    }

    [Test]
    public void ShouldHaveError_When_PhoneNumberHasIncorrectFormat()
    {
        CustomerRequest customerRequest = new();
        customerRequest.PhoneNumber = "318-231234";

        var result = validator.TestValidate(customerRequest);
        result.ShouldHaveValidationErrorFor(customer => customer.PhoneNumber);
    }

    [Test]
    public void ShouldNotHaveError_When_PhoneNumberHasCorrectFormat()
    {
        CustomerRequest customerRequest = new();
        customerRequest.PhoneNumber = "(318) 123-1234";

        var result = validator.TestValidate(customerRequest);
        result.ShouldNotHaveValidationErrorFor(customer => customer.PhoneNumber);
    }

    [TestCase("7111")]
    [TestCase("71111-12")]
    [TestCase("71111-121234")]
    [TestCase("7111132")]
    [TestCase("7111132-12345")]
    public void ShouldHaveError_When_ZipCodeHasIncorrectLength(string zip)
    {
        CustomerRequest customerRequest = new();
        customerRequest.ZipCode = zip;

        var result = validator.TestValidate(customerRequest);
        result.ShouldHaveValidationErrorFor(customer => customer.ZipCode);
    }

    [TestCase("71111-")]
    [TestCase("71a11")]
    [TestCase("71111-ab")]
    [TestCase("71111-ab12")]
    public void ShouldHaveError_When_ZipCodeHasIncorrectFormat(string zip)
    {
        CustomerRequest customerRequest = new();
        customerRequest.ZipCode = zip;

        var result = validator.TestValidate(customerRequest);
        result.ShouldHaveValidationErrorFor(customer => customer.ZipCode);
    }

    [TestCase("71111")]
    [TestCase("71111-1234")]
    public void ShouldNotHaveError_When_ZipCodeHascorrectFormat(string zip)
    {
        CustomerRequest customerRequest = new();
        customerRequest.ZipCode = zip;

        var result = validator.TestValidate(customerRequest);
        result.ShouldNotHaveValidationErrorFor(customer => customer.ZipCode);
    }

}

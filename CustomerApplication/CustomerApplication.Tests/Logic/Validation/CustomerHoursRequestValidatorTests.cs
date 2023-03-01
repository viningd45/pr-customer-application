using CustomerApplication.Logic.Validation;
using CustomerApplication.Models.RequestModel;
using FluentValidation.TestHelper;

namespace CustomerApplication.Tests.Logic.Validation;

[TestFixture]
public class CustomerHoursRequestValidatorTests
{
    private CustomerHoursRequestValidator validator;

    [SetUp]
    public void Setup()
    {
        validator = new CustomerHoursRequestValidator();
    }

    [TestCase(-1)]
    [TestCase(0)]
    public void ShouldHaveError_When_CustomerIdNotPositive(int customerId)
    {
        CustomerHoursRequest hoursRequest = new();
        hoursRequest.CustomerId = customerId;

        var result = validator.TestValidate(hoursRequest);
        result.ShouldHaveValidationErrorFor(hours => hours.CustomerId);
    }

    [TestCase("invalid date")]
    [TestCase("2022/1-3 14:20am")]
    public void ShouldHaveError_When_OpeningNotValidFormat(string opening)
    {
        CustomerHoursRequest hoursRequest = new();
        hoursRequest.Opening = opening;

        var result = validator.TestValidate(hoursRequest);
        result.ShouldHaveValidationErrorFor(hours => hours.Opening);
    }

    [TestCase("invalid date")]
    [TestCase("2022/1-3 14:20am")]
    public void ShouldHaveError_When_ClosingNotValidFormat(string closing)
    {
        CustomerHoursRequest hoursRequest = new();
        hoursRequest.Closing = closing;

        var result = validator.TestValidate(hoursRequest);
        result.ShouldHaveValidationErrorFor(hours => hours.Closing);
    }

    [Test]
    public void ShouldHaveError_When_ClosingLessThanOpening()
    {
        DateTime date = DateTime.Now;
        CustomerHoursRequest hoursRequest = new();
        hoursRequest.Opening = date.ToString();
        hoursRequest.Closing = date.AddHours(-1).ToString();

        var result = validator.TestValidate(hoursRequest);
        result.ShouldHaveValidationErrorFor(hours => hours.Closing);
    }

    [Test]
    public void ShouldNotHaveError_When_OpeningLessThanClosing()
    {
        DateTime date = DateTime.Now;
        CustomerHoursRequest hoursRequest = new();
        hoursRequest.Closing = date.ToString();
        hoursRequest.Opening = date.AddHours(-1).ToString();

        var result = validator.TestValidate(hoursRequest);
        result.ShouldNotHaveValidationErrorFor(hours => hours.Closing);
    }
}

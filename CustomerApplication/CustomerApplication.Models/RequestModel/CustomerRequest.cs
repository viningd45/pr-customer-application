using CustomerApplication.Models.DatabaseModel;
using CustomerApplication.Helpers.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApplication.Models.RequestModel;

public class CustomerRequest
{
    public CustomerRequest()
    {
    }

    public CustomerRequest(Customer customer)
    {
        if (customer != null)
        {
            this.Id = customer.Id;
            this.Name = customer.Name;
            this.PhoneNumber = customer.PhoneNumber;
            this.AddressLineOne = customer.AddressLineOne;
            this.AddressLineTwo = customer.AddressLineTwo;
            this.City = customer.City;
            this.State = customer.State;
            this.ZipCode = customer.ZipCode;
            this.ZipCodeAdditional = customer.ZipCodeAdditional;
        }
    }

    [FromQuery]
    public int Id { get; set; } = 0;
    [FromQuery]
    public string Name { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public string AddressLineOne { get; set; } = String.Empty;
    public string AddressLineTwo { get; set; } = String.Empty;
    [FromQuery]
    public string City { get; set; } = String.Empty;
    [FromQuery]
    public string State { get; set; } = String.Empty;
    [FromQuery]
    public string ZipCode { get; set; } = String.Empty;
    public string ZipCodeAdditional { get; set; } = String.Empty;
    public string PhoneNumberFormatted
    {
        get
        {
            if (Int64.TryParse(PhoneNumber, out long number) && PhoneNumber.Length == 10)
            {
                return String.Format("{0:(###) ###-####}", number);
            }
            return PhoneNumber;
        }
    }
    public string StreetAddress 
    { 
        get
        {
            return $"{AddressLineOne} {AddressLineTwo}";
        }
    }
    public string FullAddress 
    { 
        get
        {
            return $"{StreetAddress} {City}, {State} {ZipCodeFormatted}";
        }
    }

    public string ZipCodeFormatted
    {
        get
        {
            if (ZipCodeAdditional.HasValue()) return ZipCode + '-' + ZipCodeAdditional;
            return ZipCode;
        }
    }
}

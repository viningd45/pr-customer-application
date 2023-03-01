using CustomerApplication.Models.RequestModel;
using CustomerApplication.Helpers.Extensions;

namespace CustomerApplication.Models.DatabaseModel;

public class Customer
{
    public Customer()
    {
    }

    public Customer(CustomerRequest model)
    {
        this.Id = model.Id;
        this.Name = model.Name;
        this.PhoneNumber = model.PhoneNumber.GetNumbers();
        this.AddressLineOne = model.AddressLineOne;
        this.AddressLineTwo = model.AddressLineTwo;
        this.City = model.City;
        this.State = model.State;
        this.ZipCode = model.ZipCode;
        this.ZipCodeAdditional = model.ZipCodeAdditional;

        var zipPairs = model.ZipCode.Split('-');
        if (zipPairs.Length == 2 && model.ZipCode.Length > 5)
        {
            this.ZipCode = zipPairs[0].GetNumbers();
            this.ZipCodeAdditional = zipPairs[1].GetNumbers();
        }
        else
        {
            this.ZipCode = model.ZipCode;
        }
    }

    public int Id { get; set; } = 0;
    public string Name { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public string AddressLineOne { get; set; } = String.Empty;
    public string AddressLineTwo { get; set; } = String.Empty;
    public string City { get; set; } = String.Empty;
    public string State { get; set; } = String.Empty;
    public string ZipCode { get; set; } = String.Empty;
    public string ZipCodeAdditional { get; set; } = String.Empty;
}

using CustomerApplication.Models.DatabaseModel;
using CustomerApplication.Helpers.Extensions;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace CustomerApplication.DataAccess.DataServices.Dapper;

public class DapperCustomerData : DapperDataBase, ICustomerData
{
    public DapperCustomerData(IConfiguration configuration, string connectionKey = "") : base(configuration, connectionKey)
    {
    }

    public IEnumerable<Customer> GetCustomers(Customer? filter = null)
    {
        string sql = @"
                        SELECT customers.Id,
                               customers.Name,
                               customers.PhoneNumber,
                               customers.AddressLineOne,
                               customers.AddressLineTwo,
                               customers.City,
                               customers.State,
                               customers.ZipCode,
                               customers.ZipCodeAdditional
                        FROM dbo.Customers customers
                        WHERE 1 = 1
                    ";

        DynamicParameters parameters = new();

        if (filter != null)
        {
            if (filter.Id > 0)
            {
                sql += " AND customers.Id = @Id";
                parameters.Add("@Id", filter.Id);
            }

            if (filter.City.HasValue())
            {
                sql += " AND customers.City = @City";
                parameters.Add("@City", filter.City);
            }

            if (filter.State.HasValue())
            {
                sql += " AND customers.State = @State";
                parameters.Add("@State", filter.State);
            }

            if (filter.ZipCode.HasValue())
            {
                sql += " AND customers.ZipCode = @ZipCode";
                parameters.Add("@ZipCode", filter.ZipCode);
            }
        }

        using SqlConnection conn = new(ConnectionString);
        return conn.Query<Customer>(sql, parameters);
    }

    public Customer GetCustomer(int id)
    {
        Customer filter = new()
        {
            Id = id
        };

        return GetCustomers(filter)?.FirstOrDefault() ?? new();
    }

    public int UpdateCustomer(Customer customer)
    {
        string sql = @"
                    UPDATE dbo.Customers SET 
	                    Name = @Name,
	                    PhoneNumber = @PhoneNumber,
	                    AddressLineOne = @AddressLineOne,
	                    AddressLineTwo = @AddressLineTwo,
	                    City = @City,
	                    State = @State,
	                    ZipCode = @ZipCode,
	                    ZipCodeAdditional = @ZipCodeAdditional
                    WHERE Id = @Id
                ";

        using SqlConnection conn = new(ConnectionString);
        return conn.Execute(sql, customer);
    }

    public int CreateCustomer(Customer customer)
    {
        string sql = @"
                        INSERT INTO dbo.Customers
                        (
                            Name,
                            PhoneNumber,
	                        AddressLineOne,
	                        AddressLineTwo,
	                        City,
	                        State,
	                        ZipCode,
	                        ZipCodeAdditional
                        )
                        VALUES
                        (
                            @Name,
                            @PhoneNumber,
                            @AddressLineOne,
	                        @AddressLineTwo,
	                        @City,
	                        @State,
	                        @ZipCode,
	                        @ZipCodeAdditional
                        )
                    ";

        using SqlConnection conn = new(ConnectionString);
        return conn.Execute(sql, customer);
    }

    public int DeleteCustomer(int customerId)
    {
        string sql = @"
                        DELETE FROM dbo.CustomerOpenHours 
                        WHERE CustomerId = @customerId

                        DELETE FROM dbo.Customers 
                        WHERE Id = @customerId
                    ";

        using SqlConnection conn = new(ConnectionString);
        return conn.Execute(sql, new { customerId });
    }
}

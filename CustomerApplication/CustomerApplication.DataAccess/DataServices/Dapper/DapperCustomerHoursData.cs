using CustomerApplication.Models.DatabaseModel;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace CustomerApplication.DataAccess.DataServices.Dapper;

public class DapperCustomerHoursData : DapperDataBase, ICustomerHoursData
{
    public DapperCustomerHoursData(IConfiguration configuration, string connectionKey = "") : base(configuration, connectionKey)
    {
    }

    public int CreateCustomerHours(CustomerHours opening)
    {
        string sql = @"
                        INSERT INTO dbo.CustomerOpenHours
                        (
                            CustomerId,
                            Opening,
                            Closing,
                            DayOfWeek
                        )
                        VALUES
                        (   @CustomerId, -- CustomerId - int
                            @Opening, -- StartOpening - time
                            @Closing,  -- EndOpening - time
                            @DayOfWeek -- DayOfWeek - time
                        )
                    ";

        using SqlConnection conn = new(ConnectionString);
        return conn.Execute(sql, opening);
    }

    public int DeleteCustomerHours(int customerOpening)
    {
        string sql = @"
                        DELETE FROM dbo.CustomerOpenHours 
                        WHERE Id = @customerOpening
                    ";

        using SqlConnection conn = new(ConnectionString);
        return conn.Execute(sql, new { customerOpening });
    }

    public IEnumerable<CustomerHours> GetCustomerHours(int customerId)
    {
        string sql = @"
                        SELECT customerHours.Id,
                               customerHours.CustomerId,
                               customerHours.DayOfWeek,
                               customerHours.Opening,
                               customerHours.Closing 
                        FROM dbo.CustomerOpenHours customerHours 
                        WHERE customerHours.CustomerId = @customerId        
                        ORDER BY customerHours.DayOfWeek, customerHours.Opening
                    ";

        using SqlConnection conn = new(ConnectionString);
        return conn.Query<CustomerHours>(sql, new { customerId });
    }

    public int UpdateCustomerHours(CustomerHours opening)
    {
        string sql = @"
                        UPDATE dbo.CustomerOpenHours SET 
	                        Opening = @Opening,
	                        Closing = @Closing,
                            DayOfWeek = @DayOfWeek
                        WHERE Id = @Id
                    ";

        using SqlConnection conn = new(ConnectionString);
        return conn.Execute(sql, opening);
    }
}

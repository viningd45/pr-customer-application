using CustomerApplication.Models.DatabaseModel;

namespace CustomerApplication.DataAccess;

public interface ICustomerHoursData
{
    IEnumerable<CustomerHours> GetCustomerHours(int customerId);
    int CreateCustomerHours(CustomerHours opening);
    int UpdateCustomerHours(CustomerHours opening);
    int DeleteCustomerHours(int customerOpeningId);
}

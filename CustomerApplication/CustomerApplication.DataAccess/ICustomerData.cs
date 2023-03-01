using CustomerApplication.Models.DatabaseModel;

namespace CustomerApplication.DataAccess;

public interface ICustomerData
{
    IEnumerable<Customer> GetCustomers(Customer? filter = null);
    Customer GetCustomer(int id);
    int CreateCustomer(Customer customer);
    int UpdateCustomer(Customer customer);
    int DeleteCustomer(int customerId);
}

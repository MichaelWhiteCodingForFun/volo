using POD.Entities;
using System.Collections.Generic;

namespace POD.Interfaces
{
    public interface ICustomerDataService
    {
        void Create(Customer customer);
        void Delete(int customerId);
        void Edit(Customer customer);
        Customer GetCustomer(Customer customer);
        List<Customer> GetCustomers(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
        Customer GetCustomerByID(int id);
    }
}

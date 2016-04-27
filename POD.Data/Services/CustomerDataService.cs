using Dapper;
using POD.Data.Dapper;
using POD.Entities;
using POD.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POD.Data.Services
{
    public class CustomerDataService : BaseDataService, ICustomerDataService
    {
        #region Temp

      
        public static List<Customer> customers = new List<Customer>()
            {
                new Customer() { ID = 1, PayerAccountName = "Customer 1 LLC", PayerAccountNumber = "11", Email="customer1@a.com" },
                new Customer() { ID = 2, PayerAccountName = "Customer 2 LLC", PayerAccountNumber = "22", Email="customer1@a.com" },
                new Customer() { ID = 3, PayerAccountName = "Customer 3 LLC", PayerAccountNumber = "33", Email="customer1@a.com" }
            };

        public CustomerDataService(PODContext context) : base(context)
        {

        }
        /// <summary>
        /// Create Customer
        /// </summary>
        /// <param name="customer"></param>
        public void Create(Customer customer)
        {
            customers.Add(customer);
        }
        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <param name="customerId"></param>
        public void Delete(int customerId)
        {
            Customer customer = customers.FirstOrDefault(p => p.ID == customerId);
            if (customer != null)
                customers.Remove(customer);
        }
        /// <summary>
        /// Edit Customer
        /// </summary>
        /// <param name="customer"></param>
        public void Edit(Customer customer)
        {
            Customer customerToEdit = customers.FirstOrDefault(c => c.ID == customer.ID);
            if (customerToEdit != null)
            {
                customerToEdit.ID = customer.ID;
                customerToEdit.PayerAccountName = customer.PayerAccountName;
                customerToEdit.PayerAccountNumber = customer.PayerAccountNumber;
                customerToEdit.Email = customer.Email;
            }
        }

        public Customer GetCustomer(Customer customer)
        {
            Customer c = customers.FirstOrDefault(p => p.ID == customer.ID);
            return c;
        }
        /// <summary>
        /// Get Customers
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<Customer> GetCustomers(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {

            totalRows = customers.Count();

            return customers.Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList(); ;
        }


        #endregion

        #region Dapper

        public Customer GetCustomer(int Id)
        {

            ///
            /// Fetch User + Related items in one roundtrip.
            ///
            string sql = "SELECT * FROM \"customer\" WHERE ID = @id;";
            sql += "SELECT * FROM UserEmail WHERE UserId = @id;";
            sql += "SELECT * FROM ApiToken WHERE UserId = @id;";
            sql += "SELECT * FROM OrganizationMembership WHERE UserId = @id;";
            sql += "SELECT * FROM Organization WHERE Id IN (SELECT OrganizationId FROM OrganizationMembership WHERE UserId = @id);";
            using (var multi = this._db.Connection.QueryMultiple(sql, new { id = Id }))
            {
                List<Customer> ret = multi.Read<Customer>().ToList();
                if (ret.Count == 0)
                {
                    // Not found
                    return null;
                }
                Customer customer = ret[0];
                //customer.Roles = multi.Read<Role>().ToList();

                //var orgs = multi.Read<Organization>().ToDictionary(row => (Guid)row.Id, row => (Organization)row);
                //foreach (OrganizationMembership om in customer.Membership)
                //{
                //    om.Organization = orgs[om.OrganizationId];
                //}
                return customer;
            }

        }

        #endregion

        #region Dapper Extensions

        public Customer GetCustomerByID(int id)
        {
            Customer customer = null;
            try
            {
                // Basic DapperExtensions read
                 customer = this._db.Get<Customer>(new { ID = id });
                //if (res.Count() == 0)
                //{
                //    // Not found - return null
                //    return null;
                //}
                //else if (res.Count() > 1)
                //{
                //    throw new InvalidOperationException("Get by username returned more than 1 user for username: " + username);
                //}

            }
            catch (Exception ex)

            {
            }

            return customer;

        }

        #endregion

    }
}

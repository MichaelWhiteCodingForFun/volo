using Ninject;
using POD.Data;
using POD.Entities;
using POD.Interfaces;
using POD.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POD.Portal.Controllers.WebApi
{
    [RoutePrefix("api/CustomerService")]
    public class CustomerServiceController : ApiController
    {
        [Inject]
        public ICustomerDataService _customerDataService { get; set; }

        public CustomerServiceController(ICustomerDataService customerDataService)
        {
            _customerDataService = customerDataService;
        }

        /// <summary>
        /// Get Customers
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        [Route("GetCustomers")]
        [HttpPost]
        public HttpResponseMessage GetCustomers(HttpRequestMessage request, [FromBody] CustomerViewModel customerViewModel)
        {
            _customerDataService.GetCustomerByID(1);
            int currentPageNumber = customerViewModel.CurrentPageNumber;
            int pageSize = customerViewModel.PageSize;
            string sortExpression = customerViewModel.SortExpression;
            string sortDirection = customerViewModel.SortDirection;

            int totalRows;
            List<Customer> customers = _customerDataService.GetCustomers(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
            // customerViewModelList = new CustomerViewModelList();

            //foreach (Customer customer in customers)
            //{
            //    CustomerViewModel customerViewModel = customer;
            //    customerViewModelList.Add(customerViewModel);
            //}


            customerViewModel.Customers = customers;
            customerViewModel.ReturnStatus = true;

            customerViewModel.TotalRows = totalRows;
            customerViewModel.TotalPages = CalculateTotalPages(totalRows, pageSize);

            var response = Request.CreateResponse<CustomerViewModel>(HttpStatusCode.OK, customerViewModel);
            return response;

        }
        public static int CalculateTotalPages(long numberOfRecords, Int32 pageSize)
        {
            long result;
            int totalPages;

            Math.DivRem(numberOfRecords, pageSize, out result);

            if (result > 0)
                totalPages = (int)((numberOfRecords / pageSize)) + 1;
            else
                totalPages = (int)(numberOfRecords / pageSize);

            return totalPages;

        }


        [Route("Add")]
        [HttpPost]
        public HttpResponseMessage Create([FromBody] CustomerViewModel customerViewModel)
        {
            try
            {
                Customer customer = new Customer { ID = customerViewModel.CustomerID, PayerAccountName = customerViewModel.PayerAccountName, PayerAccountNumber = customerViewModel.PayerAccountNumber, Email = customerViewModel.Email };
                _customerDataService.Create(customer);
                var response = Request.CreateResponse<CustomerViewModel>(HttpStatusCode.OK, customerViewModel);
                return response;
            }
            catch
            {
                throw new Exception("ERROR : CustomerController : Line 93");
            }
        }

        [Route("Edit")]
        [HttpPost]
        public HttpResponseMessage Edit([FromBody] CustomerViewModel customerViewModel)
        {
            try
            {
                Customer customer = new Customer();
                customer.ID = customerViewModel.CustomerID;
                customer.PayerAccountName = customerViewModel.PayerAccountName;
                customer.PayerAccountNumber = customerViewModel.PayerAccountNumber;
                customer.Email = customerViewModel.Email;
                _customerDataService.Edit(customer);
                var response = Request.CreateResponse<Customer>(HttpStatusCode.OK, customer);
                return response;
            }
            catch
            {
                throw new Exception("ERROR : CustomerController : Line 112");
            }
        }

        [Route("Get")]
        [HttpPost]
        public HttpResponseMessage GetX([FromBody]CustomerViewModel customerViewModel)
        {
            //CustomerDataService customerDataService = new CustomerDataService()
            //Customer customer = customerDataService.GetCustomer();

            //if (transaction.ReturnStatus == false)
            //{
            //    customerViewModel.ReturnStatus = false;
            //    customerViewModel.ReturnMessage = transaction.ReturnMessage;
            //    customerViewModel.ValidationErrors = transaction.ValidationErrors;

            //    var responseError = Request.CreateResponse<CustomerViewModel>(HttpStatusCode.BadRequest, customerViewModel);
            //    return responseError;

            //}

            // customerViewModel.PayerAccountName = customer.PayerAccountName;
            // customerViewModel.PayerAccountNumber = customer.PayerAccountNumber;
            // customerViewModel.Email = customer.Email;

            // customerViewModel.ReturnStatus = true;
            //  customerViewModel.ReturnMessage = transaction.ReturnMessage;
            try
            {
                Customer Cus = new Customer();
                Cus.ID = customerViewModel.CustomerID;
                _customerDataService.GetCustomer(Cus);
                var response = Request.CreateResponse<CustomerViewModel>(HttpStatusCode.OK, customerViewModel);
                return response;
            }
            catch
            {
                throw new Exception("ERROR : CustomerController : Line 148");
            }
        }

        [Route("Delete")]
        [HttpPost]
        public HttpResponseMessage Delete([FromBody]CustomerViewModel customerViewModel)
        {
            try
            {
                _customerDataService.Delete(customerViewModel.CustomerID);
                var response = Request.CreateResponse<CustomerViewModel>(HttpStatusCode.OK, customerViewModel);
                return response;
            }
            catch
            {
                throw new Exception("ERROR : CustomerController : Line 186");
            }
        }
    }
}

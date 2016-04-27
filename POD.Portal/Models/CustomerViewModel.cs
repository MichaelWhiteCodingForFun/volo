using POD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POD.Portal.Models
{
    public class CustomerViewModel : BaseViewModel
    {
        public int CustomerID { get; set; }
        public string PayerAccountNumber { get; set; }
        public string PayerAccountName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public List<Customer> Customers { get; set; }

        //public static implicit operator CustomerViewModel(Customer customerEntity)
        //{
        //    return new CustomerViewModel
        //    {
        //        CustomerID = customerEntity.CustomerID,
        //        CustomerCode = customerEntity.CustomerCode,
        //        CompanyName = customerEntity.CompanyName,
        //        ContactName = customerEntity.ContactName
        //    };
        //}

        //public static implicit operator Customer(CustomerViewModel customerViewModel)
        //{
        //    return new Customer
        //    {
        //        CustomerID = customerViewModel.CustomerID,
        //        CustomerCode = customerViewModel.CustomerCode,
        //        CompanyName = customerViewModel.CompanyName,
        //        ContactName = customerViewModel.ContactName
        //    };
        //}
    }

    //public class CustomerViewModelList : BaseViewModelList
    //{
    //    public List<CustomerViewModel> Customers { get; set; }

    //}
  

}
using System;

namespace POD.Entities
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PayerAccountNumber{ get; set; }
        public string PayerAccountName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Domain { get; set; }

        //public List<Role> Roles { get; set; }

        //paging info to be moved to other class
        //public int TotalPages { get; set; }
        //public int TotalRows { get; set; }
        //public int PageSize { get; set; }
        //public Boolean IsAuthenicated { get; set; }
        //public string SortExpression { get; set; }
        //public string SortDirection { get; set; }

    }
}

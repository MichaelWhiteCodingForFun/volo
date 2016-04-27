using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POD.Portal.Models
{
    public class UserViewModel : BaseViewModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
    }

    public class UserViewModelList : BaseViewModelList
    {
        public List<UserViewModel> Users { get; set; }

    }
}
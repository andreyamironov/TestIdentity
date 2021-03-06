using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Models;

namespace TestIdentity.Identity.ViewModels
{
    public class UsersViewModel : PagerListModel<ApplicationUser>
    {
        public UsersViewModel()
        {
            base.ControllerName = "Users";
        }
        new public string QueryString
        {
            get
            {
                if (string.IsNullOrEmpty(base.QueryString)) return base.ControllerName;
                else return base.QueryString;
            }
            set
            {
                base.QueryString = value;
            }
        }
    }
}

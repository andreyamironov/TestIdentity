using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Core;
using TestIdentity.Identity.Models;

namespace TestIdentity.Identity.ViewModels
{
    public class UsersViewModel : PagerListModel<ApplicationUser>
    {
        public UsersViewModel()
        {
            base.ControllerName = "Users";
        }

        public UsersViewModel(HttpParams httpParams, int total, IEnumerable<ApplicationUser> items) : base(httpParams, total, items)
        {
            base.ControllerName = "Users";
        }
    }
}

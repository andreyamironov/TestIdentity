using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Core;
using IdentityServerAspNetIdentity.Models;

namespace IdentityServerAspNetIdentity.ViewModels
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

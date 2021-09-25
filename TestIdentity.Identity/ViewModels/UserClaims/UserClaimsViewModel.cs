using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Models;
using AMir.Wrapper;

namespace TestIdentity.Identity.ViewModels
{
    public class UserClaimsViewModel : PagerListModel<UserClaimViewModel>
    {
        public UserClaimsViewModel()
        {
            this.ControllerName = "UserClaims";
            this.Informations.SetValue("UserName", "NOT FOUND");
            this.Informations.SetValue("UserId", "NOT FOUND");
        }
    }
}

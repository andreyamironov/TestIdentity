using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Models;
using AMir.Wrapper;

namespace IdentityServerAspNetIdentity.ViewModels
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

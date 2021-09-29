using IdentityServerAspNetIdentity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class IdentityResourcesViewModel : PagerListModel<IdentityResourceViewModel>
    {
        public IdentityResourcesViewModel()
        {
            base.ControllerName = "IdentityResources";
        }
        public IdentityResourcesViewModel(HttpParams httpParams, int total, IEnumerable<IdentityResourceViewModel> items) : base(httpParams, total, items)
        {
            base.ControllerName = "IdentityResources";
        }
    }
}

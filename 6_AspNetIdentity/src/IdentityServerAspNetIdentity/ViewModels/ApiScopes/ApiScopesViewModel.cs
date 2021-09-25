using IdentityServerAspNetIdentity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class ApiScopesViewModel : PagerListModel<ApiScopeViewModel>
    {
        public ApiScopesViewModel()
        {
            base.ControllerName = "ApiScopes";
        }
        public ApiScopesViewModel(HttpParams httpParams, int total, IEnumerable<ApiScopeViewModel> items) : base(httpParams, total, items)
        {
            base.ControllerName = "ApiScopes";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Models;
using AMir.Wrapper;
using IdentityServerAspNetIdentity.Core;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class ClientScopesViewModel : PagerListModel<ClientScopeViewModel>
    {
        public ClientScopesViewModel()
        {
            this.ControllerName = "ClientScopes";
            this.Informations.SetValue("ClientId", "NOT FOUND");
            this.Informations.SetValue("ClientName", "NOT FOUND");
        }
        public ClientScopesViewModel(HttpParams httpParams, int total, IEnumerable<ClientScopeViewModel> items) : base(httpParams, total, items)
        {
            base.ControllerName = "ClientScopes";
        }
    }
}

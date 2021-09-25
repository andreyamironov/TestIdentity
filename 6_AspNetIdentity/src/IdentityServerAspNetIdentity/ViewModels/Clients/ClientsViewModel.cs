using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Core;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class ClientsViewModel:PagerListModel<ClientViewModel>
    {
        public ClientsViewModel()
         {
            base.ControllerName = "Clients";
         }
        public ClientsViewModel(HttpParams httpParams, int total,IEnumerable<ClientViewModel> items):base(httpParams,total,items)
        {
            base.ControllerName = "Clients";
        }
    }
}

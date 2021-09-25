using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Core;

namespace TestIdentity.Identity.ViewModels
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

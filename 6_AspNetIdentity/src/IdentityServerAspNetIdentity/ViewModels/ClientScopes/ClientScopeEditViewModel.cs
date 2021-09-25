using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class ClientScopeEditViewModel : ViewModelBase
    {
        [HiddenInput]
        public int ClientId { get; set; }
        [HiddenInput]
        public string ClientName { get; set; }
        public string Scope{ get; set; }
        public string ScopeOriginal { get; set; }
    }
}

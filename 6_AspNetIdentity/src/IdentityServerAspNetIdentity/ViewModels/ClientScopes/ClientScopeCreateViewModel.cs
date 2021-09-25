using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class ClientScopeCreateViewModel:ViewModelBase
    {
        [HiddenInput]
        public int Id { get; set; }
        [HiddenInput]
        public int ClientId { get; set; }
        [HiddenInput]
        public string ClientName { get; set; }
        public string Scope { get; set; }
        public ClientScopeCreateViewModel()
        {
            ClientName = "NOT FOUND";
        }
    }
}

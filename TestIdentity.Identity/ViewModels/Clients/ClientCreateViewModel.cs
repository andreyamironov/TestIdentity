using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestIdentity.Identity.ViewModels
{
    public class ClientCreateViewModel:ViewModelBase
    {
        public string ClientId { get; set; }
        
        [DataType(DataType.Password)]
        public string ClientSecrets { get; set; }
        public string AllowedGrantTypes { get; set; }
        public string AllowedScopes { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList SelectAllowedGrantTypes { get; set; }
  
        public string RedirectUris { get; set; }

        public string PostLogoutRedirectUris { get; set; }




        //var client = new IdentityServer4.Models.Client
        //{

        //    ClientId = "client_777",
        //    ClientUri = "http://my.site.com",
        //    ClientSecrets = { new IdentityServer4.Models.Secret("secret2".Sha256()) },

        //    AllowedGrantTypes = GrantTypes.ClientCredentials,
        //    // scopes that client has access to
        //    AllowedScopes = { "api22", "api3" }
        //};
    }
}

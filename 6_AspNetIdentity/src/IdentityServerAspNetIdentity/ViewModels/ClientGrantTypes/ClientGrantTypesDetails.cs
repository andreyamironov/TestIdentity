using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class ClientGrantTypesDetails : ViewModelBase
    {
        [HiddenInput]
        public int ClientId { get; set; }
        [HiddenInput]
        public string ClientName { get; set; }

        [Display(Name = "Implicit")]
        public bool IsImplicit { get; set; }
        
        [Display(Name = "Hybrid")]
        public bool IsHybrid { get; set; }
        
        [Display(Name = "AuthorizationCode")]
        public bool IsAuthorizationCode { get; set; }
        
        [Display(Name = "ClientCredentials")]
        public bool IsClientCredentials { get; set; }
        
        [Display(Name = "DeviceFlow")]
        public bool IsDeviceFlow { get; set; }
        
        [Display(Name = "ResourceOwnerPassword")]
        public bool IsResourceOwnerPassword { get; set; }

        public ClientGrantTypesDetails()
        {
            ClientName = "NOT FOUND";
        }
    }
}

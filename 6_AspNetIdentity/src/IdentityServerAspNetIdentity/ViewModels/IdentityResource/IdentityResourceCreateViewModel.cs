using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class IdentityResourceCreateViewModel : IdentityServer4.Models.IdentityResource
    {
        public string ReturnUrl_VmProperty { get; set; }
    }
}

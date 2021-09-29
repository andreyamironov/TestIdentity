using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class IdentityResourceViewModel:IdentityServer4.Models.IdentityResource
    {
        [ReadOnly(true)]
        public int Id { get; set; }
    }
}

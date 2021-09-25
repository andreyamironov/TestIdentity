using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class ApiScopesViewModel: IdentityServer4.Models.ApiScope
    {
        [ReadOnly(true)]
        public int Id { get; set; }
    }
}

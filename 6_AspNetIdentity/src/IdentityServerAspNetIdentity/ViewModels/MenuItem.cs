using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class MenuItem
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Header { get; set; }
    }
}

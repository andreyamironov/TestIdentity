using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class AccountDetailsViewModel
    {
        public string UserName { get; set; }
        public IEnumerable<KeyValuePair<string,string>> Claims { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestIdentity.Identity.ViewModels
{
    public class AccountDetailsViewModel
    {
        public string UserName { get; set; }
        public IEnumerable<KeyValuePair<string,string>> Claims { get; set; }
    }
}

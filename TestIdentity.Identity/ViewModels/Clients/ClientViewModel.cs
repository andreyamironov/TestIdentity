using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestIdentity.Identity.ViewModels
{
    public class ClientViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string AllowedScopes { get; set; }

    }
}

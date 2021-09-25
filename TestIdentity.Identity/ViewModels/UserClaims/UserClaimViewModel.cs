using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestIdentity.Identity.ViewModels
{
    public class UserClaimViewModel
    {
        [HiddenInput]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}

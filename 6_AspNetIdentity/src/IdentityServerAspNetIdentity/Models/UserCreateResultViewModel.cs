using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Models
{
    public class UserCreateResult:ApplicationUser
    {
        public string Password { get; set; }
        public IdentityResult Result { get; set; }
    }
}

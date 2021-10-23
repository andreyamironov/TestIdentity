using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.ViewModels.Users
{
    public class UserCreateResultViewModel:ApplicationUser
    {
        public IdentityResult Result { get; set; }
    }
}

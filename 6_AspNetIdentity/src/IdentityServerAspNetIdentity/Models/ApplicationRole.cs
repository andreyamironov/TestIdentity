using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole(string name) : base(name)
        {
        }
    }
}

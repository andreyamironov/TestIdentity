using AMir.Interface.Data;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Repository.Users
{
    public class UsersReaderRepository : IReaderRepository<ApplicationUser, Guid>
    {
        UserManager<ApplicationUser> _userManager;

        public UsersReaderRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUser Get(Func<ApplicationUser, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetList(Func<ApplicationUser, bool> predicate, int skip, int take, out int total)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(Func<ApplicationUser, bool> predicate, ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
    }
}

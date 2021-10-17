using AMir.Interface.Data;
using AMir.Wrapper;
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
            return _userManager.Users.FirstOrDefault(predicate);
        }

        public IEnumerable<ApplicationUser> GetList(Func<ApplicationUser, bool> predicate, int skip, int take, out int total)
        {
            return _userManager.Users.SkipTakeFuncPredicate(predicate, skip, take, out total);
        }

        public int IndexOf(Func<ApplicationUser, bool> predicate, ApplicationUser entity)
        {
            if (predicate != null)
                return _userManager.Users.Where(predicate).IndexOf(entity);
            else
                return _userManager.Users.IndexOf(entity);
        }
    }
}

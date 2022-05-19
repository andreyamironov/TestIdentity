using AMir.Interface.Data;
using AMir.Wrapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Models;

namespace TestIdentity.Identity.Repository
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

        public IEnumerable<ApplicationUser> GetList(Func<ApplicationUser, object> orderByKeySelector, Func<ApplicationUser, bool> whereKeySelector, int skip, int take, out int total, string orderByAscDesc = "asc")
        {
            return _userManager.Users.SkipTakeFuncPredicate(orderByKeySelector,whereKeySelector, skip, take, out total, orderByAscDesc);
        }

        public int IndexOf(Func<ApplicationUser, bool> predicate, ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
    }
}

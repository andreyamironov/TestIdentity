using AMir.Interface.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Models;

namespace TestIdentity.Identity.Repository
{
    public class UsersReaderRepository : IReaderRepository<ApplicationUser, Guid>
    {
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

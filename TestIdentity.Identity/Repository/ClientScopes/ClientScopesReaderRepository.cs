using AMir.Interface.Data;
using AMir.Wrapper;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestIdentity.Identity.Repository.ClientScopes
{
    public class ClientScopesReaderRepository : IdentityServerRepositoryBase, IReaderByOwnerRepository<int,ClientScope, int>
    {
        public ClientScopesReaderRepository(ConfigurationDbContext configurationDbContext) : base(configurationDbContext)
        {

        }

        public ClientScope Get(int ownerEntityKey,Func<ClientScope, bool> predicate)
        {
            var result = _configurationDbContext.Clients
                    .AsQueryable().Include(c => c.AllowedScopes)
                    .FirstOrDefaultAsync(c => c.Id == ownerEntityKey).Result.AllowedScopes.Where(predicate).FirstOrDefault(); ;
            return result;
        }

        public IEnumerable<ClientScope> GetList(int ownerEntityKey,Func<ClientScope, bool> predicate, int skip, int take, out int total)
        {
            total = 0;
            var result = _configurationDbContext.Clients
                     .AsQueryable().Include(c => c.AllowedScopes)
                     .FirstOrDefaultAsync(c => c.Id ==ownerEntityKey).Result
                     ?.AllowedScopes
                     .OrderBy(a=>a.Scope)
                     .SkipTakeFuncPredicate(predicate, skip, take, out total);
            return result;
        }

        public int IndexOf(int ownerEntityKey,Func<ClientScope, bool> predicate, ClientScope entity)
        {
            throw new NotImplementedException();
        }
    }
}

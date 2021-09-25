using AMir.Interface.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestIdentity.Identity.Repository.ClientGrantTypes
{
    public class ClientGrantTypesReaderRepository : IdentityServerRepositoryBase, IReaderByOwnerRepository<int, ClientGrantType, int>
    {
        public ClientGrantTypesReaderRepository(ConfigurationDbContext configurationDbContext) : base(configurationDbContext)
        {

        }

        public ClientGrantType Get(int ownerEntityKey, Func<ClientGrantType, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClientGrantType> GetList(int ownerEntityKey, Func<ClientGrantType, bool> predicate, int skip, int take, out int total)
        {
            total = 0;
            var result = _configurationDbContext.Clients
                     .AsQueryable().Include(c => c.AllowedGrantTypes)
                     .FirstOrDefaultAsync(c => c.Id == ownerEntityKey).Result
                     ?.AllowedGrantTypes;
            return result;
        }

        public int IndexOf(int ownerEntityKey, Func<ClientGrantType, bool> predicate, ClientGrantType entity)
        {
            throw new NotImplementedException();
        }
    }
}

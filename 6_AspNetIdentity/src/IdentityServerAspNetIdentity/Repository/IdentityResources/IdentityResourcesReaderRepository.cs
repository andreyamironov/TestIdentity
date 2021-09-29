using AMir.Interface.Data;
using AMir.Wrapper;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Repository.IdentityResources
{
    public class IdentityResourcesReaderRepository : IdentityServerRepositoryBase, IReaderRepository<IdentityResource, int>
    {
        public IdentityResourcesReaderRepository(ConfigurationDbContext configurationDbContext) : base(configurationDbContext)
        {
        }

        public IdentityResource Get(Func<IdentityResource, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IdentityResource> GetList(Func<IdentityResource, bool> predicate, int skip, int take, out int total)
        {
            return _configurationDbContext.IdentityResources.SkipTakeFuncPredicate(predicate, skip, take, out total);
        }

        public int IndexOf(Func<IdentityResource, bool> predicate, IdentityResource entity)
        {
            throw new NotImplementedException();
        }
    }
}

using AMir.Interface.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using AMir.Wrapper;
using System.Linq;

namespace IdentityServerAspNetIdentity.Repository.ApiScopes
{
    public class ApiScopesReaderRepository : IdentityServerRepositoryBase, IReaderRepository<ApiScope, int>
    {
        public ApiScopesReaderRepository(ConfigurationDbContext configurationDbContext) : base(configurationDbContext)
        {
        }

        public ApiScope Get(Func<ApiScope, bool> predicate)
        {
            return _configurationDbContext.ApiScopes.FirstOrDefault(predicate);
        }

        public IEnumerable<ApiScope> GetList(Func<ApiScope, bool> predicate, int skip, int take, out int total)
        {
            return _configurationDbContext.ApiScopes.SkipTakeFuncPredicate(predicate, skip, take, out total);
        }

        public int IndexOf(Func<ApiScope, bool> predicate, ApiScope entity)
        {
            if (predicate != null)
                return _configurationDbContext.ApiScopes.Where(predicate).IndexOf(entity);
            else
                return _configurationDbContext.ApiScopes.IndexOf(entity);
        }
    }
}

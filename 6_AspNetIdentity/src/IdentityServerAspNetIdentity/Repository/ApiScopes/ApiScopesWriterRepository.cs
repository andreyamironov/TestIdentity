using AMir.Interface.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Repository.ApiScopes
{
    public class ApiScopesWriterRepository : IdentityServerRepositoryBase, IWriterRepository<ApiScope>
    {
        public ApiScopesWriterRepository(ConfigurationDbContext configurationDbContext) : base(configurationDbContext)
        {
        }

        public ApiScope Create(ApiScope entity)
        {
            _configurationDbContext.ApiScopes.Add(entity);
            _configurationDbContext.SaveChanges();
            return entity;
        }

        public void Delete(ApiScope entity)
        {
            throw new NotImplementedException();
        }

        public ApiScope Update(ApiScope originalEntity, ApiScope source = null)
        {
            throw new NotImplementedException();
        }
    }
}

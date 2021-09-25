using IdentityServer4.EntityFramework.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Repository
{
    public abstract class IdentityServerRepositoryBase
    {
        protected ConfigurationDbContext _configurationDbContext;
        public IdentityServerRepositoryBase(ConfigurationDbContext configurationDbContext)
        {
            _configurationDbContext = configurationDbContext;
        }
    }
}

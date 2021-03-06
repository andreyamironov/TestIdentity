using AMir.Interface.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Repository.IdentityResources
{
    public class IdentityResourcesWriterRepository : IdentityServerRepositoryBase, IWriterRepository<IdentityResource>
    {
        public IdentityResourcesWriterRepository(ConfigurationDbContext configurationDbContext) : base(configurationDbContext)
        {
        }

        public IdentityResource Create(IdentityResource entity)
        {
            _configurationDbContext.IdentityResources.Add(entity);
            _configurationDbContext.SaveChanges();
            return entity;
        }

        public void Delete(IdentityResource entity)
        {
            _configurationDbContext.IdentityResources.Remove(entity);
            _configurationDbContext.SaveChanges();
        }

        public IdentityResource Update(IdentityResource originalEntity, IdentityResource source = null)
        {
            if (source == null) _configurationDbContext.IdentityResources.Update(originalEntity);
            else
            {
                source.Id = originalEntity.Id;
                _configurationDbContext.Entry(originalEntity).CurrentValues.SetValues(source);
            }
            _configurationDbContext.SaveChanges();

            return originalEntity;
        }
    }
}

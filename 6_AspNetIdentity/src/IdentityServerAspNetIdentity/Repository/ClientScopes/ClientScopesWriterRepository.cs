using AMir.Interface.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Repository.ClientScopes
{
    public class ClientScopesWriterRepository : IdentityServerRepositoryBase, IWriterRepository<ClientScope>
    {
        public ClientScopesWriterRepository(ConfigurationDbContext configurationDbContext) : base(configurationDbContext)
        {
        }

        public ClientScope Create(ClientScope entity)
        {
            _configurationDbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _configurationDbContext.SaveChanges();


            //var find = _configurationDbContext.Clients.FirstOrDefault(e => e.Id == entity.ClientId);
            //if(find!=null)
            //{
            //    find.AllowedScopes.Add(entity);
            //    _configurationDbContext.SaveChanges();
            //}
            return entity;
        }

        public void Delete(ClientScope entity)
        {
            _configurationDbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _configurationDbContext.SaveChanges();
        }

        public ClientScope Update(ClientScope originalEntity, ClientScope source = null)
        {
            //if (source == null) _configurationDbContext.Clients.Update(originalEntity);
            //else
            //{
                source.Id = originalEntity.Id;
                _configurationDbContext.Entry(originalEntity).CurrentValues.SetValues(source);
            //}
            _configurationDbContext.SaveChanges();

            return originalEntity;
        }
    }
}

using AMir.Interface.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Repository.Clients
{
    public class ClientsWriterRepository :IdentityServerRepositoryBase, IWriterRepository<Client>
    {
        public ClientsWriterRepository(ConfigurationDbContext configurationDbContext) : base(configurationDbContext)
        {

        }

        public Client Create(Client entity)
        {
            _configurationDbContext.Clients.Add(entity);
            _configurationDbContext.SaveChanges();
            return entity;
        }

        public void Delete(Client entity)
        {
            _configurationDbContext.Clients.Remove(entity);
            _configurationDbContext.SaveChanges();
        }

        public Client Update(Client originalEntity, Client source = null)
        {
            if(source == null) _configurationDbContext.Clients.Update(originalEntity);
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

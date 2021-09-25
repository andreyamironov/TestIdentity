using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AMir.Wrapper;
using AMir.Interface.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerAspNetIdentity.Repository.Clients
{
    public class ClientsReaderRepository : IdentityServerRepositoryBase,IReaderRepository<Client, int>
    {
        public ClientsReaderRepository(ConfigurationDbContext configurationDbContext):base(configurationDbContext)
        {

        }

        public Client Get(Func<Client, bool> predicate)
        {
            return _configurationDbContext.Clients.FirstOrDefault(predicate);
        }

        public IEnumerable<Client> GetList(Func<Client, bool> predicate, int skip, int take,out int total)
        {
            return _configurationDbContext.Clients.Include(s=>s.AllowedScopes).SkipTakeFuncPredicate(predicate, skip, take,out total);
        }

        public int IndexOf(Func<Client, bool> predicate, Client entity)
        {
            if(predicate != null)
                return _configurationDbContext.Clients.Where(predicate).IndexOf(entity);
            else
                return _configurationDbContext.Clients.IndexOf(entity);
        }
    }
}

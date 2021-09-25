using System;
using System.Collections.Generic;
using System.Linq;
using AMir.Interface.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerAspNetIdentity.Repository
{
    public class IdentityServerClientsRepository : IRepositoryOld<Client, int>
    {
        ConfigurationDbContext _context;

        public IdentityServerClientsRepository(ConfigurationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Client> Get(Func<Client, bool> predicate = null)
        {
            if (predicate == null) return _context.Clients;
            else return _context.Clients.Where(predicate);
        }
        public Client Get(int id)
        {
            return _context.Clients.Find(id);
        }

        public void Create(Client item)
        {
            _context.Clients.Add(item);
        }
        public void Update(Client item)
        {
            item.Id = 6;
            var find  = _context.Clients.FirstOrDefault(c => c.ClientId == item.ClientId);


            if (find != null)
            {
                _context.Entry(find).CurrentValues.SetValues(item);
                _context.Clients.Update(find);
            }
        }
        public void Delete(int id)
        {
            var find = Get(id);
            if (find != null) _context.Clients.Remove(find);
        }
   
        public void Save()
        {
            var db = _context as DbContext;
            db.SaveChanges();
        }     
    }
}

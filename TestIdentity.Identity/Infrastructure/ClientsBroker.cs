using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.ViewModels;
using IdentityServer4.Models;
using IdentityServer4.EntityFramework.Mappers;
using AMir.Interface.Data;
using AutoMapper;

namespace TestIdentity.Identity.Infrastructure
{
    public class ClientsBroker:IRepositoryOld<ViewModels.ClientViewModel,string>
    {
        IWebHostEnvironment _appEnvironment;
        ConfigurationDbContext _configurationDbContext;
        IMapper _mapper;

        public ClientsBroker(IWebHostEnvironment appEnvironment, ConfigurationDbContext configurationDbContext,IMapper mapper)
        {
            _appEnvironment = appEnvironment;
            _configurationDbContext = configurationDbContext;
            _mapper = mapper;
        }

        public void Create(ClientViewModel clientViewModel)
        {
            IdentityServer4.Models.Client client = new IdentityServer4.Models.Client
            {
                ClientId = "client",
                ClientSecrets = { new IdentityServer4.Models.Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "api1" }
            };

            _configurationDbContext.Clients.Add(client.ToEntity());
            _configurationDbContext.SaveChanges();
            ;
        }
        public void Update(ClientViewModel item)
        {
            throw new NotImplementedException();
        }
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClientViewModel> Get(Func<ClientViewModel, bool> predicate)
        {
            var clientDb = _configurationDbContext.Clients;

            var map = _mapper.Map<IEnumerable<IdentityServer4.EntityFramework.Entities.Client>,IEnumerable<ClientViewModel>>(clientDb);

            return map;
        }

        public ClientViewModel Get(string id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

    }
}

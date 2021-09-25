using AMir.Interface.Data;
using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestIdentity.Identity.Queries.ClientGrantTypes;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Handlers.ClientGrantTypes
{
    public class DetailsClientGrantTypesGetHandler : IRequestHandler<DetailsClientGrantTypesGetQuery, ClientGrantTypesDetails>
    {
        IReaderByOwnerRepository<int, ClientGrantType, int> _reader;
        IMapper _mapper;

        public DetailsClientGrantTypesGetHandler(IReaderByOwnerRepository<int, ClientGrantType, int> reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }
        public Task<ClientGrantTypesDetails> Handle(DetailsClientGrantTypesGetQuery request, CancellationToken cancellationToken)
        {
            var model = new ClientGrantTypesDetails();

            int total = 0;
            var dbEntities = _reader.GetList(request.ClientId,null,0,0,out total);
            IdentityServer4.EntityFramework.Entities.Client client = dbEntities?.FirstOrDefault()?.Client;

            model.ClientId = client.Id;
            model.ClientName = client.ClientId;

            foreach(var itm in dbEntities)
            {
                if (string.Compare(itm.GrantType, GrantType.AuthorizationCode) == 0) model.IsAuthorizationCode = true;
                else if (string.Compare(itm.GrantType, GrantType.ClientCredentials) == 0) model.IsClientCredentials = true;
                else if (string.Compare(itm.GrantType, GrantType.DeviceFlow) == 0) model.IsDeviceFlow = true;
                else if (string.Compare(itm.GrantType, GrantType.Hybrid) == 0) model.IsHybrid = true;
                else if (string.Compare(itm.GrantType, GrantType.Implicit) == 0) model.IsImplicit = true;
                else if (string.Compare(itm.GrantType, GrantType.ResourceOwnerPassword) == 0) model.IsResourceOwnerPassword = true;
            }

            return Task.FromResult(model);
        }
    }
}

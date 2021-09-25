using AMir.Interface.Data;
using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Queries.ClientScopes;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Handlers.ClientScopes
{
    public class CreateClientScopeGetHandler : IRequestHandler<CreateClientScopetGetQuery, ClientScopeCreateViewModel>
    {
        IReaderRepository<Client, int> _reader;
        IMapper _mapper;

        public CreateClientScopeGetHandler(IReaderRepository<Client, int> reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }
        public Task<ClientScopeCreateViewModel> Handle(CreateClientScopetGetQuery request, CancellationToken cancellationToken)
        {
            ClientScopeCreateViewModel model = new ClientScopeCreateViewModel();

            var client = _reader.Get(c => c.Id == request.ClientId);
            if(client != null)
            {
                model.ClientId = client.Id;
                model.ClientName = client.ClientId;
            }
            return Task.FromResult(model);
        }
    }
}

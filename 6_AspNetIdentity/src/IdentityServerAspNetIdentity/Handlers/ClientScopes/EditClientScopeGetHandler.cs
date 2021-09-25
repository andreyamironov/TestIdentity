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
    public class EditClientScopeGetHandler : IRequestHandler<EditClientScopeGetQuery, ClientScopeEditViewModel>
    {
        IReaderByOwnerRepository<int, ClientScope, int> _reader;
        IMapper _mapper;

        public EditClientScopeGetHandler(IReaderByOwnerRepository<int, ClientScope, int> reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }
        public Task<ClientScopeEditViewModel> Handle(EditClientScopeGetQuery request, CancellationToken cancellationToken)
        {
            var dbEntities = _reader.Get(request.ClientId, c => c.Scope == request.Scope);
            var mapModels = _mapper.Map<IdentityServer4.EntityFramework.Entities.ClientScope, ClientScopeEditViewModel>(dbEntities);
            return Task.FromResult(mapModels);
        }
    }
}

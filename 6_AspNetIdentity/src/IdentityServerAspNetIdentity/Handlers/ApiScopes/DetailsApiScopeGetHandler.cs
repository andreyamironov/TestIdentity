using AMir.Interface.Data;
using AutoMapper;
using IdentityServerAspNetIdentity.Queries.ApiScopes;
using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Handlers.ApiScopes
{
    public class DetailsApiScopeGetHandler : IRequestHandler<DetailsApiScopeGetQuery, ApiScopeDetailsViewModel>
    {
        IReaderRepository<IdentityServer4.EntityFramework.Entities.ApiScope, int> _reader;
        IMapper _mapper;

        public DetailsApiScopeGetHandler(IReaderRepository<IdentityServer4.EntityFramework.Entities.ApiScope, int> reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }

        public Task<ApiScopeDetailsViewModel> Handle(DetailsApiScopeGetQuery request, CancellationToken cancellationToken)
        {
            var dbEntity = _reader.Get(c => c.Id == request.Id);
            var mapModel = _mapper.Map<IdentityServer4.EntityFramework.Entities.ApiScope, ApiScopeDetailsViewModel>(dbEntity);
            return Task.FromResult(mapModel);
        }
    }
}

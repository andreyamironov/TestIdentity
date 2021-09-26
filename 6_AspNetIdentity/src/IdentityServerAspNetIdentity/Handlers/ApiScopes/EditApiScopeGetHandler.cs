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
    public class EditApiScopeGetHandler : IRequestHandler<EditApiScopeGetQuery, ApiScopeEditViewModel>
    {
        IReaderRepository<IdentityServer4.EntityFramework.Entities.ApiScope, int> _reader;
        IMapper _mapper;

        public EditApiScopeGetHandler(IReaderRepository<IdentityServer4.EntityFramework.Entities.ApiScope, int> reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }

        public Task<ApiScopeEditViewModel> Handle(EditApiScopeGetQuery request, CancellationToken cancellationToken)
        {
            var dbEntity = _reader.Get(c => c.Id == request.Id);
            var mapModel = _mapper.Map<IdentityServer4.EntityFramework.Entities.ApiScope, ApiScopeEditViewModel>(dbEntity);
            return Task.FromResult(mapModel);
        }
    }
}

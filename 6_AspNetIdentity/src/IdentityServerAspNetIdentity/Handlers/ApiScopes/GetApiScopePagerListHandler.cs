using AMir.Interface.Data;
using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
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
    public class GetApiScopePagerListHandler : IRequestHandler<GetApiScopesPagerListQuery, PagerListModel<ApiScopesViewModel>>
    {
        IReaderRepository<ApiScope, int> _reader;
        IMapper _mapper;

        public GetApiScopePagerListHandler(IReaderRepository<ApiScope, int> reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;   
        }
        public Task<PagerListModel<ApiScopesViewModel>> Handle(GetApiScopesPagerListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

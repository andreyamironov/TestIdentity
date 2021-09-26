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
    public class CreateApiScopeGetHandler : IRequestHandler<CreateApiScopeGetQuery, ApiScopeCreateViewModel>
    {
        public Task<ApiScopeCreateViewModel> Handle(CreateApiScopeGetQuery request, CancellationToken cancellationToken)
        {
            if (request.ApiScopeCreateViewModel == null) return Task.FromResult(new ApiScopeCreateViewModel());
            else return Task.FromResult(request.ApiScopeCreateViewModel);
        }
    }
}

using IdentityServerAspNetIdentity.Queries.IdentityResource;
using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Handlers.IdentityResources
{
    public class CreateIdentityResourceGetHandler : IRequestHandler<CreateIdentityResourceGetQuery, IdentityResourceCreateViewModel>
    {
        public Task<IdentityResourceCreateViewModel> Handle(CreateIdentityResourceGetQuery request, CancellationToken cancellationToken)
        {
            if (request.IdentityResourceViewModel == null) return Task.FromResult(new IdentityResourceCreateViewModel());
            else return Task.FromResult(request.IdentityResourceViewModel);
        }
    }
}

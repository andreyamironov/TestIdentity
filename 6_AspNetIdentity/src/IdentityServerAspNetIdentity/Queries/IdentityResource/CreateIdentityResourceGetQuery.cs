using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Queries.IdentityResource
{
    public record CreateIdentityResourceGetQuery(IdentityResourceCreateViewModel IdentityResourceViewModel):IRequest<IdentityResourceCreateViewModel>;
}

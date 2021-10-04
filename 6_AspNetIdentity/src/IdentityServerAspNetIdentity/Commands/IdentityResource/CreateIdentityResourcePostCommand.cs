using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Commands.IdentityResource
{
    public record CreateIdentityResourcePostCommand(IdentityResourceCreateViewModel IdentityResourceCreateViewModel) : IRequest<IdentityResourceViewModel>;
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Commands.IdentityResource
{
    public record DeleteIdentityResourcePostCommand(int Id) : IRequest<bool>;
}

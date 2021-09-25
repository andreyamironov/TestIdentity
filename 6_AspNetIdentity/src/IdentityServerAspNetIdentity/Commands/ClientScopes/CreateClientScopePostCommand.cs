using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Commands.ClientScopes
{
    public record CreateClientScopePostCommand(ClientScopeCreateViewModel ClientScopeCreateViewModel) :IRequest<ClientScopeCreateViewModel>;
}

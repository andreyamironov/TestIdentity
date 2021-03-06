using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Commands.ClientScopes
{
    public record CreateClientScopePostCommand(ClientScopeCreateViewModel ClientScopeCreateViewModel) :IRequest<ClientScopeCreateViewModel>;
}

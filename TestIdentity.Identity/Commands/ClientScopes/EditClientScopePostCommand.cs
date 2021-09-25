using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TestIdentity.Identity.ViewModels;


namespace TestIdentity.Identity.Commands.ClientScopes
{
    public record EditClientScopePostCommand(ClientScopeEditViewModel ClientScopeEditViewModel):IRequest<ClientScopeEditViewModel>;
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using IdentityServerAspNetIdentity.ViewModels;


namespace IdentityServerAspNetIdentity.Commands.ClientScopes
{
    public record EditClientScopePostCommand(ClientScopeEditViewModel ClientScopeEditViewModel):IRequest<ClientScopeEditViewModel>;
}

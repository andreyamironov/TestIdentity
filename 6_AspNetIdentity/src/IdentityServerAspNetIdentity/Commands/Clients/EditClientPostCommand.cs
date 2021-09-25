using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Commands.Clients
{
    public record EditClientPostCommand(ClientEditViewModel ClientEditViewModel) : IRequest<ClientEditViewModel>;
}

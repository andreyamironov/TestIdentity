using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Queries.ClientScopes
{
    public record CreateClientScopetGetQuery(int ClientId) : IRequest<ClientScopeCreateViewModel>;
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Queries.Clients
{
    public record CreateClientGetQuery(ClientCreateViewModel CreateClientViewModel) : IRequest<ClientCreateViewModel>;
}

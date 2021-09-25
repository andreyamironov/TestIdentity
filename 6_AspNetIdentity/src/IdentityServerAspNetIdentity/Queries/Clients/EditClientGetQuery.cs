using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Queries.Clients
{
    public record EditClientGetQuery(int Id) : IRequest<ClientEditViewModel>;
}

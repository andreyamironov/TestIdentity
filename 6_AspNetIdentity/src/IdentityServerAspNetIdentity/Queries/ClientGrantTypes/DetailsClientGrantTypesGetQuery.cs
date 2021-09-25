using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Queries.ClientGrantTypes
{
    public record DetailsClientGrantTypesGetQuery(int ClientId) : IRequest<ClientGrantTypesDetails>;
}

using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Queries.Users
{
    public record EditUserGetQuery(string Id) : IRequest<UserEditViewModel>;
}

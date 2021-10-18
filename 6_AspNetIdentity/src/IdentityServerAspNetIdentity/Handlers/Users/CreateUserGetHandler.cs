using IdentityServerAspNetIdentity.Queries.Users;
using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Handlers.Users
{
    public class CreateUserGetHandler : IRequestHandler<CreateUserGetQuery, UserCreateViewModel>
    {
        public Task<UserCreateViewModel> Handle(CreateUserGetQuery request, CancellationToken cancellationToken)
        {
            if (request.UserCreateViewModel == null) return Task.FromResult(new UserCreateViewModel());
            else return Task.FromResult(request.UserCreateViewModel);
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestIdentity.Identity.Queries.Users;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Handlers.Users
{
    public class CreateUserGetHandler : IRequestHandler<CreateUserGetQuery, UserCreateViewModel>
    {
        public Task<UserCreateViewModel> Handle(CreateUserGetQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(request.UserCreateViewModel);
        }
    }
}

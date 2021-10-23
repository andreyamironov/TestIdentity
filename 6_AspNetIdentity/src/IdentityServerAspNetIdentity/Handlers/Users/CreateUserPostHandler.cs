using IdentityServerAspNetIdentity.Commands.Users;
using IdentityServerAspNetIdentity.Models;
using IdentityServerAspNetIdentity.ViewModels.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Handlers.Users
{
    public class CreateUserPostHandler : IRequestHandler<CreateUserPostCommand, UserCreateResultViewModel>
    {
        public Task<UserCreateResultViewModel> Handle(CreateUserPostCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

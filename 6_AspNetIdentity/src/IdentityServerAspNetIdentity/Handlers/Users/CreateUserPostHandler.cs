using IdentityServerAspNetIdentity.Commands.Users;
using IdentityServerAspNetIdentity.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Handlers.Users
{
    public class CreateUserPostHandler : IRequestHandler<CreateUserPostCommand, ApplicationUser>
    {
        public Task<ApplicationUser> Handle(CreateUserPostCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

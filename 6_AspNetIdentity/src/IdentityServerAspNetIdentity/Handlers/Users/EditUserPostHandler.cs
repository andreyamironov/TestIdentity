using IdentityServerAspNetIdentity.Commands.Users;
using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Handlers.Users
{
    public class EditUserPostHandler : IRequestHandler<EditUserPostCommand, UserEditViewModel>
    {

        public Task<UserEditViewModel> Handle(EditUserPostCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

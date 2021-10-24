using AMir.Interface.Data;
using IdentityServerAspNetIdentity.Models;
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
    public class EditUserGetHandler : IRequestHandler<EditUserGetQuery, UserEditViewModel>
    {
        IReaderRepository<ApplicationUser, Guid> _reader;

        public EditUserGetHandler(IReaderRepository<ApplicationUser, Guid> reader)
        {
            _reader = reader;
        }

        public Task<UserEditViewModel> Handle(EditUserGetQuery request, CancellationToken cancellationToken)
        {
            var dbEntity = _reader.Get(c => c.Id.ToString() == request.Id);

            return Task.FromResult(new UserEditViewModel()
            {
                Id = dbEntity.Id.ToString(),
                EMail = dbEntity.Email,
                EmailConfirmed = dbEntity.EmailConfirmed
            });
        }
    }
}

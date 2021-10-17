using AMir.Interface.Data;
using AutoMapper;
using IdentityServerAspNetIdentity.Models;
using IdentityServerAspNetIdentity.Queries.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Handlers.Users
{
    public class DetailsUserGetHandler : IRequestHandler<DetailsUserGetQuery, IdentityServerAspNetIdentity.Models.ApplicationUser>
    {
        IReaderRepository<IdentityServerAspNetIdentity.Models.ApplicationUser, Guid> _reader;

        public DetailsUserGetHandler(IReaderRepository<IdentityServerAspNetIdentity.Models.ApplicationUser, Guid> reader)
        {
            _reader = reader;
        }

        public Task<ApplicationUser> Handle(DetailsUserGetQuery request, CancellationToken cancellationToken)
        {
            var dbEntity = _reader.Get(c => c.Id == request.Id);
            return Task.FromResult(dbEntity);
        }
    }
}


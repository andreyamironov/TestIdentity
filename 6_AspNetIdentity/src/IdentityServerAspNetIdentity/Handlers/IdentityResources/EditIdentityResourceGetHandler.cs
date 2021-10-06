using AMir.Interface.Data;
using AutoMapper;
using IdentityServerAspNetIdentity.Queries.IdentityResource;
using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Handlers.IdentityResources
{
    public class EditIdentityResourceGetHandler : IRequestHandler<EditIdentityResourceGetQuery, IdentityResourceEditViewModel>
    {
        IReaderRepository<IdentityServer4.EntityFramework.Entities.IdentityResource, int> _reader;
        IMapper _mapper;

        public Task<IdentityResourceEditViewModel> Handle(EditIdentityResourceGetQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

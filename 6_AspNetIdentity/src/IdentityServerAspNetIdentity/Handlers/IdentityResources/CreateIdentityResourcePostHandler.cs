using AMir.Interface.Data;
using AutoMapper;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServerAspNetIdentity.Commands.IdentityResource;
using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Handlers.IdentityResources
{
    public class CreateIdentityResourcePostHandler : IRequestHandler<CreateIdentityResourcePostCommand, IdentityResourceViewModel>
    {
        IWriterRepository<IdentityServer4.EntityFramework.Entities.IdentityResource> _writer;
        IMapper _mapper;

        public CreateIdentityResourcePostHandler(IWriterRepository<IdentityServer4.EntityFramework.Entities.IdentityResource> writer, IMapper mapper)
        {
            _writer = writer;
            _mapper = mapper;
        }

        public Task<IdentityResourceViewModel> Handle(CreateIdentityResourcePostCommand request, CancellationToken cancellationToken)
        {
            IdentityResourceCreateViewModel apiScopeCreateViewModel = request.IdentityResourceCreateViewModel;
            IdentityServer4.Models.IdentityResource identityResource =
                _mapper.Map<IdentityResourceCreateViewModel, IdentityServer4.Models.IdentityResource>(apiScopeCreateViewModel);

            var entity = _writer.Create(identityResource.ToEntity());
            var entityToViewModel = _mapper.Map<IdentityServer4.EntityFramework.Entities.IdentityResource, IdentityResourceViewModel>(entity);
            return Task.FromResult(entityToViewModel);
        }
    }
}

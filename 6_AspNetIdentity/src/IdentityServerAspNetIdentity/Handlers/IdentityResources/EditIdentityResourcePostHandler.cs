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
    public class EditIdentityResourcePostHandler : IRequestHandler<EditIdentityResourcePostCommand, IdentityResourceEditViewModel>
    {
        IReaderRepository<IdentityServer4.EntityFramework.Entities.IdentityResource, int> _reader;
        IWriterRepository<IdentityServer4.EntityFramework.Entities.IdentityResource> _writer;
        IMapper _mapper;

        public EditIdentityResourcePostHandler(IReaderRepository<IdentityServer4.EntityFramework.Entities.IdentityResource, int> reader, IWriterRepository<IdentityServer4.EntityFramework.Entities.IdentityResource> writer, IMapper mapper)
        {
            _reader = reader;
            _writer = writer;
            _mapper = mapper;
        }

        public Task<IdentityResourceEditViewModel> Handle(EditIdentityResourcePostCommand request, CancellationToken cancellationToken)
        {
            IdentityServer4.Models.IdentityResource identityResource  = request.IdentityResourceEditViewModel;
            var sourse = identityResource.ToEntity();

            var originalEntity = _reader.Get(c => c.Id == request.IdentityResourceEditViewModel.Id);

            var editEntity = _writer.Update(originalEntity, sourse);

            var entityToViewModel = _mapper.Map<IdentityServer4.EntityFramework.Entities.IdentityResource, IdentityResourceEditViewModel>(editEntity);

            return Task.FromResult(entityToViewModel);
        }
    }
}

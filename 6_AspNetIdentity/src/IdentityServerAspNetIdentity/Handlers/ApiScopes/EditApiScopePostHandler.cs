using AMir.Interface.Data;
using AutoMapper;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServerAspNetIdentity.Commands.ApiScopes;
using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Handlers.ApiScopes
{
    public class EditApiScopePostHandler : IRequestHandler<EditApiScopePostCommand, ApiScopeEditViewModel>
    {
        IReaderRepository<IdentityServer4.EntityFramework.Entities.ApiScope, int> _reader;
        IWriterRepository<IdentityServer4.EntityFramework.Entities.ApiScope> _writer;
        IMapper _mapper;

        public EditApiScopePostHandler(IReaderRepository<IdentityServer4.EntityFramework.Entities.ApiScope, int> reader, IWriterRepository<IdentityServer4.EntityFramework.Entities.ApiScope> writer, IMapper mapper)
        {
            _reader = reader;
            _writer = writer;
            _mapper = mapper;
        }

        public Task<ApiScopeEditViewModel> Handle(EditApiScopePostCommand request, CancellationToken cancellationToken)
        {
            IdentityServer4.Models.ApiScope apiScope = request.ApiScopeEditViewModel;
            var sourse = apiScope.ToEntity();

            var originalEntity = _reader.Get(c => c.Id == request.ApiScopeEditViewModel.Id);

            var editEntity = _writer.Update(originalEntity, sourse);

            var entityToViewModel = _mapper.Map<IdentityServer4.EntityFramework.Entities.ApiScope, ApiScopeEditViewModel>(editEntity);

            return Task.FromResult(entityToViewModel);
        }
    }
}

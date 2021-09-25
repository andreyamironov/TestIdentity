using AMir.Interface.Data;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestIdentity.Identity.Commands.ClientScopes;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Handlers.ClientScopes
{
    public class EditClientScopePostHandler : IRequestHandler<EditClientScopePostCommand, ClientScopeEditViewModel>
    {
        IReaderByOwnerRepository<int,IdentityServer4.EntityFramework.Entities.ClientScope, int> _reader;
        IWriterRepository<IdentityServer4.EntityFramework.Entities.ClientScope> _writer;
        IMapper _mapper;

        public EditClientScopePostHandler(IReaderByOwnerRepository<int,IdentityServer4.EntityFramework.Entities.ClientScope, int> reader, IWriterRepository<IdentityServer4.EntityFramework.Entities.ClientScope> writer, IMapper mapper)
        {
            _reader = reader;
            _writer = writer;
            _mapper = mapper;
        }

        public Task<ClientScopeEditViewModel> Handle(EditClientScopePostCommand request, CancellationToken cancellationToken)
        {
            ClientScopeEditViewModel clientScopeEditViewModel = request.ClientScopeEditViewModel;

            var source = _mapper.Map<ClientScopeEditViewModel,IdentityServer4.EntityFramework.Entities.ClientScope>(clientScopeEditViewModel);

            var originalEntity = _reader.Get(clientScopeEditViewModel.ClientId, c => c.Scope == clientScopeEditViewModel.ScopeOriginal);
            source.ClientId = originalEntity.ClientId;
            
            var editEntity = _writer.Update(originalEntity, source);

            var entityToViewModel = _mapper.Map<IdentityServer4.EntityFramework.Entities.ClientScope, ClientScopeEditViewModel>(editEntity);

            return Task.FromResult(entityToViewModel);
        }
    }
}

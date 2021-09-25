using AMir.Interface.Data;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Commands.ClientScopes;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Handlers.ClientScopes
{
    public class CreateClientScopePostHandler : IRequestHandler<CreateClientScopePostCommand, ClientScopeCreateViewModel>
    {
        IWriterRepository<IdentityServer4.EntityFramework.Entities.ClientScope> _writer;
        IMapper _mapper;

        public CreateClientScopePostHandler(IWriterRepository<IdentityServer4.EntityFramework.Entities.ClientScope> writer, IMapper mapper)
        {
            _writer = writer;
            _mapper = mapper;
        }

        public Task<ClientScopeCreateViewModel> Handle(CreateClientScopePostCommand request, CancellationToken cancellationToken)
        {
            ClientScopeCreateViewModel clientScopeEditViewModel = request.ClientScopeCreateViewModel;

            var source = _mapper.Map<ClientScopeCreateViewModel, IdentityServer4.EntityFramework.Entities.ClientScope>(clientScopeEditViewModel);

            var editEntity = _writer.Create(source);

            var entityToViewModel = _mapper.Map<IdentityServer4.EntityFramework.Entities.ClientScope, ClientScopeCreateViewModel>(editEntity);

            return Task.FromResult(entityToViewModel);
        }
    }
}

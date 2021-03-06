using AMir.Interface.Data;
using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Commands.Clients;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Handlers.Clients
{
    public class CreateClientPostHandler : IRequestHandler<CreateClientPostCommand, ClientViewModel>
    {
        IWriterRepository<IdentityServer4.EntityFramework.Entities.Client> _writer;
        IMapper _mapper;

        public CreateClientPostHandler(IWriterRepository<IdentityServer4.EntityFramework.Entities.Client> writer, IMapper mapper)
        {
            _writer = writer;
            _mapper = mapper;
        }

        public Task<ClientViewModel> Handle(CreateClientPostCommand request, CancellationToken cancellationToken)
        {
            ClientCreateViewModel createClientViewModel = request.CreateClientViewModel;
            IdentityServer4.Models.Client client = 
                _mapper.Map<ClientCreateViewModel, IdentityServer4.Models.Client>(createClientViewModel);


            var entity = _writer.Create(client.ToEntity());
            var entityToViewModel = _mapper.Map<IdentityServer4.EntityFramework.Entities.Client, ClientViewModel>(entity);
            return Task.FromResult(entityToViewModel);
        }
    }
}

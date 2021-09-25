using AMir.Interface.Data;
using AutoMapper;
using IdentityServer4.EntityFramework.Mappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Commands.Clients;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Handlers.Clients
{
    public class EditClientPostHandler : IRequestHandler<EditClientPostCommand, ClientEditViewModel>
    {
        IReaderRepository<IdentityServer4.EntityFramework.Entities.Client, int> _reader;
        IWriterRepository<IdentityServer4.EntityFramework.Entities.Client> _writer;
        IMapper _mapper;

        public EditClientPostHandler(IReaderRepository<IdentityServer4.EntityFramework.Entities.Client, int> reader, IWriterRepository<IdentityServer4.EntityFramework.Entities.Client> writer, IMapper mapper)
        {
            _reader = reader;
            _writer = writer;
            _mapper = mapper;
        }

        public Task<ClientEditViewModel> Handle(EditClientPostCommand request, CancellationToken cancellationToken)
        {
            ClientEditViewModel clientEditViewModel = request.ClientEditViewModel;

            IdentityServer4.Models.Client client = clientEditViewModel;
            var sourse = client.ToEntity();

            var originalEntity = _reader.Get(c => c.Id == clientEditViewModel.Id);
           
            var editEntity = _writer.Update(originalEntity,sourse);

            var entityToViewModel = _mapper.Map<IdentityServer4.EntityFramework.Entities.Client, ClientEditViewModel>(editEntity);

            return Task.FromResult(entityToViewModel);
        }
    }
}

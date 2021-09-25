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
    public class DeleteClientPostHandler : IRequestHandler<DeleteClientPostCommand, bool>
    {
        IReaderRepository<IdentityServer4.EntityFramework.Entities.Client, int> _reader;
        IWriterRepository<IdentityServer4.EntityFramework.Entities.Client> _writer;
        IMapper _mapper;

        public DeleteClientPostHandler(IReaderRepository<IdentityServer4.EntityFramework.Entities.Client, int> reader, IWriterRepository<IdentityServer4.EntityFramework.Entities.Client> writer, IMapper mapper)
        {
            _reader = reader;
            _writer = writer;
            _mapper = mapper;
        }

        public Task<bool> Handle(DeleteClientPostCommand request, CancellationToken cancellationToken)
        {  
            var originalEntity  = _reader.Get(c => c.Id == request.id);

            bool deleteResult = false;
            try
            {
                _writer.Delete(originalEntity);
                deleteResult = true;
            }
            catch { }

            return Task.FromResult(deleteResult);
        }
    }
}

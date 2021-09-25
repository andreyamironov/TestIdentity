using AMir.Interface.Data;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Commands.ClientScopes;

namespace IdentityServerAspNetIdentity.Handlers.ClientScopes
{
    public class DeleteClientScopePostHandler : IRequestHandler<DeleteClientScopePostCommand, bool>
    {
        IReaderByOwnerRepository<int, IdentityServer4.EntityFramework.Entities.ClientScope, int> _reader;
        IWriterRepository<IdentityServer4.EntityFramework.Entities.ClientScope> _writer;
        IMapper _mapper;

        public DeleteClientScopePostHandler(IReaderByOwnerRepository<int, IdentityServer4.EntityFramework.Entities.ClientScope, int> reader, IWriterRepository<IdentityServer4.EntityFramework.Entities.ClientScope> writer, IMapper mapper)
        {
            _reader = reader;
            _writer = writer;
            _mapper = mapper;
        }

        public Task<bool> Handle(DeleteClientScopePostCommand request, CancellationToken cancellationToken)
        {
            var originalEntity = _reader.Get( request.ClientScopeEditViewModel.ClientId, c => c.Scope == request.ClientScopeEditViewModel.Scope);

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

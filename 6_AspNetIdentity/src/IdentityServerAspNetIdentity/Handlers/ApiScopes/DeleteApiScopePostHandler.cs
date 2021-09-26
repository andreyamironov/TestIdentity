using AMir.Interface.Data;
using AutoMapper;
using IdentityServerAspNetIdentity.Commands.ApiScopes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Handlers.ApiScopes
{
    public class DeleteApiScopePostHandler : IRequestHandler<DeleteApiScopePostCommand, bool>
    {
        IReaderRepository<IdentityServer4.EntityFramework.Entities.ApiScope, int> _reader;
        IWriterRepository<IdentityServer4.EntityFramework.Entities.ApiScope> _writer;
        IMapper _mapper;

        public DeleteApiScopePostHandler(IReaderRepository<IdentityServer4.EntityFramework.Entities.ApiScope, int> reader, IWriterRepository<IdentityServer4.EntityFramework.Entities.ApiScope> writer, IMapper mapper)
        {
            _reader = reader;
            _writer = writer;
            _mapper = mapper;
        }

        public Task<bool> Handle(DeleteApiScopePostCommand request, CancellationToken cancellationToken)
        {
            var originalEntity = _reader.Get(c => c.Id == request.id);

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

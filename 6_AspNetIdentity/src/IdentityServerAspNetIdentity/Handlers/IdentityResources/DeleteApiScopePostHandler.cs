using AMir.Interface.Data;
using AutoMapper;
using IdentityServerAspNetIdentity.Commands.IdentityResource;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Handlers.IdentityResources
{
    public class DeleteApiScopePostHandler : IRequestHandler<DeleteIdentityResourcePostCommand, bool>
    {
        IReaderRepository<IdentityServer4.EntityFramework.Entities.IdentityResource, int> _reader;
        IWriterRepository<IdentityServer4.EntityFramework.Entities.IdentityResource> _writer;
        IMapper _mapper;

        public DeleteApiScopePostHandler(IReaderRepository<IdentityServer4.EntityFramework.Entities.IdentityResource, int> reader, IWriterRepository<IdentityServer4.EntityFramework.Entities.IdentityResource> writer, IMapper mapper)
        {
            _reader = reader;
            _writer = writer;
            _mapper = mapper;
        }

        public Task<bool> Handle(DeleteIdentityResourcePostCommand request, CancellationToken cancellationToken)
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

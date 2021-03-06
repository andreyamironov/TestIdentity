using AMir.Interface.Data;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestIdentity.Identity.Commands.Clients;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Handlers.Clients
{
    public class DetailsClientGetHandler : IRequestHandler<DetailsClientGetQuery, ClientDetailsViewModel>
    {

        IReaderRepository<IdentityServer4.EntityFramework.Entities.Client, int> _reader;
        IMapper _mapper;

        public DetailsClientGetHandler(IReaderRepository<IdentityServer4.EntityFramework.Entities.Client, int> reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }

        public Task<ClientDetailsViewModel> Handle(DetailsClientGetQuery request, CancellationToken cancellationToken)
        {
            var dbEntities = _reader.Get(c => c.Id == request.Id);
            var mapModels = _mapper.Map<IdentityServer4.EntityFramework.Entities.Client, ClientDetailsViewModel>(dbEntities);
            return Task.FromResult(mapModels);
        }
    }
}

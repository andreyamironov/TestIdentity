using AMir.Interface.Data;
using AutoMapper;
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
            var dbEntity = _reader.Get(c => c.Id == request.Id);
            var mapModel = _mapper.Map<IdentityServer4.EntityFramework.Entities.Client, ClientDetailsViewModel>(dbEntity);
            return Task.FromResult(mapModel);
        }
    }
}

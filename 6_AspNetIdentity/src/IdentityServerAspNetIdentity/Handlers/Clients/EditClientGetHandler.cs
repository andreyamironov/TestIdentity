using AMir.Interface.Data;
using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Core;
using IdentityServerAspNetIdentity.Queries.Clients;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Handlers.Clients
{
    public class EditClientGetHandler : IRequestHandler<EditClientGetQuery, ClientEditViewModel>
    {
        IReaderRepository<IdentityServer4.EntityFramework.Entities.Client, int> _reader;
        IMapper _mapper;

        public EditClientGetHandler(IReaderRepository<IdentityServer4.EntityFramework.Entities.Client, int> reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }

        public Task<ClientEditViewModel> Handle(EditClientGetQuery request, CancellationToken cancellationToken)
        {
            var dbEntitiy  = _reader.Get(c=>c.Id == request.Id);
            var mapModel   = _mapper.Map<IdentityServer4.EntityFramework.Entities.Client, ClientEditViewModel>(dbEntitiy);

            mapModel.SelectAccessTokenTypes = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(Enum.GetNames(typeof(AccessTokenType)));
            mapModel.SelectTokenExpirations = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(Enum.GetNames(typeof(TokenExpiration)));
            mapModel.SelectTokenUsages = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(Enum.GetNames(typeof(TokenUsage)));

            return Task.FromResult(mapModel);
        }
    }
}

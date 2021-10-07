﻿using AMir.Interface.Data;
using AutoMapper;
using IdentityServerAspNetIdentity.Queries.IdentityResource;
using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Handlers.IdentityResources
{
    public class EditIdentityResourceGetHandler : IRequestHandler<EditIdentityResourceGetQuery, IdentityResourceEditViewModel>
    {
        IReaderRepository<IdentityServer4.EntityFramework.Entities.IdentityResource, int> _reader;
        IMapper _mapper;

        public EditIdentityResourceGetHandler(IReaderRepository<IdentityServer4.EntityFramework.Entities.IdentityResource, int> reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }

        public Task<IdentityResourceEditViewModel> Handle(EditIdentityResourceGetQuery request, CancellationToken cancellationToken)
        {
            var dbEntity = _reader.Get(c => c.Id == request.Id);
            var mapModel = _mapper.Map<IdentityServer4.EntityFramework.Entities.IdentityResource, IdentityResourceEditViewModel>(dbEntity);
            return Task.FromResult(mapModel);
        }
    }
}

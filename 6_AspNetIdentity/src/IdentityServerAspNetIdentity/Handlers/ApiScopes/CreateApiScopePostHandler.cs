using AMir.Interface.Data;
using AutoMapper;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServerAspNetIdentity.Commands.ApiScopes;
using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Handlers.ApiScopes
{
    public class CreateApiScopePostHandler : IRequestHandler<CreateApiScopePostCommand, ApiScopeViewModel>
    {
        IWriterRepository<IdentityServer4.EntityFramework.Entities.ApiScope> _writer;
        IMapper _mapper;

        public CreateApiScopePostHandler(IWriterRepository<IdentityServer4.EntityFramework.Entities.ApiScope> writer, IMapper mapper)
        {
            _writer = writer;
            _mapper = mapper;
        }

        public Task<ApiScopeViewModel> Handle(CreateApiScopePostCommand request, CancellationToken cancellationToken)
        {
            ApiScopeCreateViewModel apiScopeCreateViewModel  = request.ApiScopeCreateViewModel;
            IdentityServer4.Models.ApiScope apiScope =
                _mapper.Map<ApiScopeCreateViewModel, IdentityServer4.Models.ApiScope>(apiScopeCreateViewModel);

            var entity = _writer.Create(apiScope.ToEntity());
            var entityToViewModel = _mapper.Map<IdentityServer4.EntityFramework.Entities.ApiScope, ApiScopeViewModel>(entity);
            return Task.FromResult(entityToViewModel);
        }
    }
}

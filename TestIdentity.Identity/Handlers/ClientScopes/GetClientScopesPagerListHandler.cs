using AMir.Interface.Data;
using AMir.Wrapper;
using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestIdentity.Identity.Core;
using TestIdentity.Identity.Queries.ClientScopes;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Handlers.ClientScopes
{
    public class GetClientScopesPagerListHandler : IRequestHandler<GetClientsScopesPagerListQuery, PagerListModel<ClientScopeViewModel>>
    {
        IReaderByOwnerRepository<int,ClientScope, int> _reader;
        IMapper _mapper;

        public GetClientScopesPagerListHandler(IReaderByOwnerRepository<int, ClientScope, int> reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }

        public Task<PagerListModel<ClientScopeViewModel>> Handle(GetClientsScopesPagerListQuery request, CancellationToken cancellationToken)
        {
            var httpParams = request.HttpParams;
            int clientId = request.ClientId;

            Func<IdentityServer4.EntityFramework.Entities.ClientScope, bool> predicate = (c) 
                => (string.IsNullOrWhiteSpace(httpParams.Search) ? true : c.Scope.Contains(httpParams.Search));

            int selectedIndex = 0;
            HttpParams.CalculateSkipTake(httpParams, out int skip, out int take, selectedIndex);

            var dbEntities = _reader.GetList(clientId, predicate, skip, take, out int total);
            Client client = dbEntities?.FirstOrDefault()?.Client;

            var mapModels = _mapper.Map<IEnumerable<IdentityServer4.EntityFramework.Entities.ClientScope>, IEnumerable<ClientScopeViewModel>>(dbEntities);

            PagerListModel<ClientScopeViewModel> pagerListModel = new ClientScopesViewModel(httpParams, total, mapModels);

            if (client != null)
            { 
                pagerListModel.Informations.SetValue("ClientId", client.ClientId);
                pagerListModel.Informations.SetValue("Id", client.Id);
            }

            return Task.FromResult(pagerListModel);
        }
    }
}

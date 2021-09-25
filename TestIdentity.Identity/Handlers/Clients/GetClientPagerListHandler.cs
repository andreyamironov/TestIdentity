using AMir.Interface.Data;
using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TestIdentity.Identity.Core;
using TestIdentity.Identity.Queries.Clients;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Handlers.Clients
{
    public class GetClientPagerListHandler : IRequestHandler<GetClientsPagerListQuery, PagerListModel<ClientViewModel>>
    {
        IReaderRepository<Client, int> _reader;
        IMapper _mapper;

        public GetClientPagerListHandler(IReaderRepository<Client,int> reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }
        public Task<PagerListModel<ClientViewModel>> Handle(GetClientsPagerListQuery request, CancellationToken cancellationToken)
        {

            var httpParams = request.HttpParams;
            Func<IdentityServer4.EntityFramework.Entities.Client, bool> predicate = (c) =>
            (string.IsNullOrWhiteSpace(httpParams.Search) ? true : c.ClientId.Contains(httpParams.Search));

            int selectedIndexClient = 0 ;
            if (httpParams.SelectedId != null)
            {
                int castId;
                bool isCastId = int.TryParse(httpParams.SelectedId, out castId);
                if (isCastId)
                {
                    var getClient = _reader.Get(c => c.Id == castId);
                    if (getClient != null)
                        selectedIndexClient = _reader.IndexOf(predicate, getClient);
                }
            }

            HttpParams.CalculateSkipTake(httpParams, out int skip, out int take, selectedIndexClient);

            var dbEntities = _reader.GetList(predicate, skip,take,out int total);
            var mapModels = _mapper.Map<IEnumerable<IdentityServer4.EntityFramework.Entities.Client>, IEnumerable<ClientViewModel>>(dbEntities);
            
            PagerListModel<ClientViewModel> pagerListModel = new ClientsViewModel(httpParams,total,mapModels);

            return Task.FromResult(pagerListModel);
        }
    }
}

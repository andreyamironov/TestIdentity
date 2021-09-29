using AMir.Interface.Data;
using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using IdentityServerAspNetIdentity.Core;
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
    public class GetIdentotyResourcesPagerListHandler : IRequestHandler<GetIdentityResourcesPagerListQuery, PagerListModel<IdentityResourceViewModel>>
    {
        IReaderRepository<IdentityResource, int> _reader;
        IMapper _mapper;

        public GetIdentotyResourcesPagerListHandler(IReaderRepository<IdentityResource, int> reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }
        public Task<PagerListModel<IdentityResourceViewModel>> Handle(GetIdentityResourcesPagerListQuery request, CancellationToken cancellationToken)
        {
            var httpParams = request.HttpParams;
            Func<IdentityServer4.EntityFramework.Entities.IdentityResource, bool> predicate = (c) =>
            (string.IsNullOrWhiteSpace(httpParams.Search) ? true : c.Name.Contains(httpParams.Search));

            int selectedIndexClient = 0;
            if (httpParams.SelectedId != null)
            {
                int castId;
                bool isCastId = int.TryParse(httpParams.SelectedId, out castId);
                if (isCastId)
                {
                    var getEntity = _reader.Get(c => c.Id == castId);
                    if (getEntity != null)
                        selectedIndexClient = _reader.IndexOf(predicate, getEntity);
                }
            }

            HttpParams.CalculateSkipTake(httpParams, out int skip, out int take, selectedIndexClient);

            var dbEntities = _reader.GetList(predicate, skip, take, out int total);
            var mapModels = _mapper.Map<IEnumerable<IdentityServer4.EntityFramework.Entities.IdentityResource>, IEnumerable<IdentityResourceViewModel>>(dbEntities);

            PagerListModel<IdentityResourceViewModel> pagerListModel = new IdentityResourcesViewModel(httpParams, total, mapModels);

            return Task.FromResult(pagerListModel);
        }
    }
}

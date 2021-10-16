using AMir.Interface.Data;
using AutoMapper;
using IdentityServerAspNetIdentity.Core;
using IdentityServerAspNetIdentity.Models;
using IdentityServerAspNetIdentity.Queries.Users;
using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Handlers.Users
{
    public class GetUsersListHandler : IRequestHandler<GetUsersPagerListQuery, PagerListModel<ApplicationUser>>
    {
        IReaderRepository<ApplicationUser, Guid> _reader;
        //IMapper _mapper;

        public GetUsersListHandler(IReaderRepository<ApplicationUser, Guid> reader)//, IMapper mapper)
        {
            _reader = reader;
            //_mapper = mapper;
        }

        public Task<PagerListModel<ApplicationUser>> Handle(GetUsersPagerListQuery request, CancellationToken cancellationToken)
        {
            var httpParams = request.HttpParams;
            Func<ApplicationUser, bool> predicate = (c) =>
            (string.IsNullOrWhiteSpace(httpParams.Search) ? true : c.Email.Contains(httpParams.Search));

            int selectedIndexClient = 0;
            if (httpParams.SelectedId != null)
            {
                    var getEntity = _reader.Get(c => c.Id == httpParams.SelectedId);
                    if (getEntity != null)
                        selectedIndexClient = _reader.IndexOf(predicate, getEntity);             
            }

            HttpParams.CalculateSkipTake(httpParams, out int skip, out int take, selectedIndexClient);

            var dbEntities = _reader.GetList(predicate, skip, take, out int total);

            PagerListModel<ApplicationUser> pagerListModel = new UsersViewModel(httpParams, total, dbEntities);

            return Task.FromResult(pagerListModel);
        }
    }
}

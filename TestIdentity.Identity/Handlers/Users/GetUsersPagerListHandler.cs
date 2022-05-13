﻿using AMir.Interface.Data;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestIdentity.Identity.Core;
using TestIdentity.Identity.Models;
using TestIdentity.Identity.Queries.Users;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Handlers.Users
{
    public class GetUsersPagerListHandler : IRequestHandler<GetUsersPagerListQuery, PagerListModel<ApplicationUser>>
    {
        IReaderRepository<ApplicationUser, Guid> _reader;
        IMapper _mapper;

        public GetUsersPagerListHandler(IReaderRepository<ApplicationUser, Guid> reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }

        public Task<PagerListModel<ApplicationUser>> Handle(GetUsersPagerListQuery request, CancellationToken cancellationToken)
        {
            HttpParams httpParams = request.HttpParams;

            Func<ApplicationUser, bool> predicate = (c) =>
            (string.IsNullOrWhiteSpace(request.HttpParams.Search) ? true : c.UserName.Contains(request.HttpParams.Search));

            HttpParams.CalculateSkipTake(httpParams, out int skip, out int take, 0);

            var dbEntities = _reader.GetList(predicate, skip, take, out int total);
            //var mapModels = _mapper.Map<IEnumerable<LogEvent>, IEnumerable<LogEventViewModel>>(dbEntities);

            PagerListModel<ApplicationUser> pagerListModel = new UsersViewModel(httpParams, total, dbEntities);

            return Task.FromResult(pagerListModel);
        }
    }
}
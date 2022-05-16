using AMir.Interface.Data;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestIdentity.Identity.Core;
using TestIdentity.Identity.Models;
using TestIdentity.Identity.Queries.LogEvents;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Handlers.LogEvents
{
    public class LogEventsPagerListGetHandler : IRequestHandler<GetLogEventsPagerListQuery, PagerListModel<LogEventViewModel>>
    {
        IReaderRepository<LogEvent, int> _reader;
        IMapper _mapper;

        public LogEventsPagerListGetHandler(IReaderRepository<LogEvent, int> reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }

        public Task<PagerListModel<LogEventViewModel>> Handle(GetLogEventsPagerListQuery request, CancellationToken cancellationToken)
        {
            HttpParams httpParams = request.HttpParams;

            Func<LogEvent, bool> predicate = (c) =>
            (string.IsNullOrWhiteSpace(request.HttpParams.Search) ? true : c.Message.Contains(request.HttpParams.Search));

            HttpParams.CalculateSkipTake(httpParams, out int skip, out int take,0);

            var dbEntities = _reader.GetList(predicate, skip, take, out int total);
            var mapModels = _mapper.Map<IEnumerable<LogEvent>, IEnumerable<LogEventViewModel>>(dbEntities);

            PagerListModel<LogEventViewModel> pagerListModel = new LogEventsViewModel(httpParams, total, mapModels);

            return Task.FromResult(pagerListModel);
        }
    }
}

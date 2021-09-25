using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Queries.LogEvents
{
    public record GetLogEventsPagerListQuery(TestIdentity.Identity.Core.HttpParams HttpParams) : IRequest<PagerListModel<TestIdentity.Identity.ViewModels.LogEventViewModel>>;
}

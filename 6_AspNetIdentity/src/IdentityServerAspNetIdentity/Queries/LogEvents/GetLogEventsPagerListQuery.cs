using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Queries.LogEvents
{
    public record GetLogEventsPagerListQuery(IdentityServerAspNetIdentity.Core.HttpParams HttpParams) : IRequest<PagerListModel<IdentityServerAspNetIdentity.ViewModels.LogEventViewModel>>;
}

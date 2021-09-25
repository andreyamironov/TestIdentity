using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Queries.ApiScopes
{
    public record GetApiScopesPagerListQuery(IdentityServerAspNetIdentity.Core.HttpParams HttpParams) : IRequest<PagerListModel<ApiScopesViewModel>>;
}

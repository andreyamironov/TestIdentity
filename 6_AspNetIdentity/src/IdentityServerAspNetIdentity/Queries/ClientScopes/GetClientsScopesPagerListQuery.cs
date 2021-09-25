using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Queries.ClientScopes
{
    public record GetClientsScopesPagerListQuery(IdentityServerAspNetIdentity.Core.HttpParams HttpParams,int ClientId) : IRequest<PagerListModel<ClientScopeViewModel>>;
}

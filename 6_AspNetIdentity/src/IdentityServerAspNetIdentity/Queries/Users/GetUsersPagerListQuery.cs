using IdentityServerAspNetIdentity.Models;
using IdentityServerAspNetIdentity.ViewModels;
using MediatR;

namespace IdentityServerAspNetIdentity.Queries.Users
{
    public record GetUsersPagerListQuery(IdentityServerAspNetIdentity.Core.HttpParams HttpParams) : IRequest<PagerListModel<ApplicationUser>>;
}

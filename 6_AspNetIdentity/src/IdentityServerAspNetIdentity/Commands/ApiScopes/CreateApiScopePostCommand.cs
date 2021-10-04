using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Commands.ApiScopes
{
    public record CreateApiScopePostCommand(ApiScopeCreateViewModel ApiScopeCreateViewModel) : IRequest<ApiScopeViewModel>;
}

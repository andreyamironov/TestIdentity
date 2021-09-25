using IdentityServer4.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Queries.Clients;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Handlers.Clients
{
    public class CreateClientGetHandler : IRequestHandler<CreateClientGetQuery, ClientCreateViewModel>
    {

        public Task<ClientCreateViewModel> Handle(CreateClientGetQuery request, CancellationToken cancellationToken)
        {
            var model = request.CreateClientViewModel;
            if (model == null) model = new ClientCreateViewModel();

            List<string> grantTypes = new List<string>();
            grantTypes.Add("");

            grantTypes.Add(GrantType.Implicit);
            grantTypes.Add(GrantType.Hybrid);
            grantTypes.Add(GrantType.AuthorizationCode);
            grantTypes.Add(GrantType.ClientCredentials);
            grantTypes.Add(GrantType.DeviceFlow);
            grantTypes.Add(GrantType.ResourceOwnerPassword);

            model.SelectAllowedGrantTypes = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(grantTypes);

            return Task.FromResult(model);
        }
    }
}

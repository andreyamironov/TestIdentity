using IdentityServerAspNetIdentity.ViewModels;
using MediatR;

namespace IdentityServerAspNetIdentity.Commands.Clients
{
    public record DetailsClientGetQuery(int Id) : IRequest<ClientDetailsViewModel>;
}

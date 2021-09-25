using TestIdentity.Identity.ViewModels;
using MediatR;

namespace TestIdentity.Identity.Commands.Clients
{
    public record DetailsClientGetQuery(int Id) : IRequest<ClientDetailsViewModel>;
}

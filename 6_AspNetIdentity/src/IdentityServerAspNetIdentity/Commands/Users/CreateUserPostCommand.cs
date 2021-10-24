using IdentityServerAspNetIdentity.Models;
using IdentityServerAspNetIdentity.ViewModels;
using MediatR;

namespace IdentityServerAspNetIdentity.Commands.Users
{
    public record CreateUserPostCommand(UserCreateViewModel UserCreateViewModel) : IRequest<UserCreateResult>;
}

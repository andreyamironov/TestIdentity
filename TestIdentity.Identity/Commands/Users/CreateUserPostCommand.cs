using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Models;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Commands.Users
{
    public record CreateUserPostCommand(UserCreateViewModel CreateClientViewModel) : IRequest<ApplicationUser>;

}

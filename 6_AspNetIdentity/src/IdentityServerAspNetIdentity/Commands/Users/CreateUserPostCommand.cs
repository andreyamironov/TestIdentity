using IdentityServerAspNetIdentity.Models;
using IdentityServerAspNetIdentity.ViewModels;
using IdentityServerAspNetIdentity.ViewModels.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Commands.Users
{
    public record CreateUserPostCommand(UserCreateViewModel UserCreateViewModel) : IRequest<UserCreateResultViewModel>;
}

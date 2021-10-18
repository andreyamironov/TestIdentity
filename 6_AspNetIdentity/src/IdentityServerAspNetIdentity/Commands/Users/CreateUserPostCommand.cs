﻿using IdentityServerAspNetIdentity.Models;
using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Commands.Users
{
    public record CreateUserPostCommand(UserCreateViewModel UserCreateViewModel) : IRequest<ApplicationUser>;
}

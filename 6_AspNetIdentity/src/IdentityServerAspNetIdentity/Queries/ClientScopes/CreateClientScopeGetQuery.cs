﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Queries.ClientScopes
{
    public record CreateClientScopetGetQuery(int ClientId) : IRequest<ClientScopeCreateViewModel>;
}

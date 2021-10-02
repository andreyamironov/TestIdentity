﻿using IdentityServerAspNetIdentity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Mapping
{
    public class IdentityResourcesMapperConfiguration : MapperConfigurationBase
    {
        public IdentityResourcesMapperConfiguration()
        {
            CreateMap<IdentityServer4.EntityFramework.Entities.IdentityResource, IdentityResourceViewModel>();
        }
    }
}
using IdentityServerAspNetIdentity.ViewModels;
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
            CreateMap<IdentityResourceCreateViewModel, IdentityServer4.EntityFramework.Entities.IdentityResource>();
            CreateMap<IdentityServer4.EntityFramework.Entities.IdentityResource, IdentityResourceEditViewModel>();
            CreateMap<IdentityServer4.EntityFramework.Entities.IdentityResource, IdentityResourceDetailsViewModel>();
        }
    }
}

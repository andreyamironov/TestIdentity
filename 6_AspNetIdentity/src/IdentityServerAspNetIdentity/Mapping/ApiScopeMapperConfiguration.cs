using IdentityServerAspNetIdentity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Mapping
{
    public class ApiScopeMapperConfiguration : MapperConfigurationBase
    {
        public ApiScopeMapperConfiguration()
        {
            CreateMap<IdentityServer4.EntityFramework.Entities.ApiScope, ApiScopeViewModel>();
        }
    }
}

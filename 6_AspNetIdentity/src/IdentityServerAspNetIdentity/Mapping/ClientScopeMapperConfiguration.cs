using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Mapping
{
    public class ClientScopeMapperConfiguration : MapperConfigurationBase
    {
        public ClientScopeMapperConfiguration()
        {
            CreateMap<IdentityServer4.EntityFramework.Entities.ClientScope, ClientScopeViewModel>()
                .ForMember(dest => dest.ClientName, act => act.MapFrom(src => src.Client != null ? src.Client.ClientId : "----"));

            CreateMap<IdentityServer4.EntityFramework.Entities.ClientScope, ClientScopeEditViewModel>()
                .ForMember(dest => dest.ClientName, act => act.MapFrom(src => src.Client != null ? src.Client.ClientId : "----"));

            CreateMap<ClientScopeCreateViewModel, IdentityServer4.EntityFramework.Entities.ClientScope>();
            CreateMap<IdentityServer4.EntityFramework.Entities.ClientScope, ClientScopeCreateViewModel>();

            CreateMap<ClientScopeEditViewModel, IdentityServer4.EntityFramework.Entities.ClientScope>();

        }
    }
}

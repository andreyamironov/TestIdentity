using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Mapping
{
    public class ClientMapperConfiguration : MapperConfigurationBase
    {
        readonly char[] SEPARATORS = new char[] {',',';' };
        public ClientMapperConfiguration()
        {
            CreateMap<IdentityServer4.EntityFramework.Entities.Client, ClientViewModel>()
                .ForMember(dest => dest.AllowedScopes, act => act.MapFrom(src => src!=null? string.Join("; ",src.AllowedScopes.Select(s=>s.Scope)) : "----"));

            CreateMap<IdentityServer4.EntityFramework.Entities.Client, ClientCreateViewModel>();
            CreateMap<IdentityServer4.EntityFramework.Entities.Client, ClientScopeEditViewModel>();
            CreateMap<IdentityServer4.EntityFramework.Entities.Client, ClientDetailsViewModel>();
            CreateMap<IdentityServer4.EntityFramework.Entities.Client, ClientEditViewModel>();


            CreateMap<ClientCreateViewModel, IdentityServer4.Models.Client>()
                .ForMember(dest => dest.ClientSecrets, act => act.MapFrom(src => new IdentityServer4.Models.Secret[] { new IdentityServer4.Models.Secret() { Value = src.ClientSecrets.Sha256() } }))
                .ForMember(dest => dest.AllowedGrantTypes, act => act.MapFrom(src => src.AllowedGrantTypes!=null ? new string[] { src.AllowedGrantTypes } : null))
                .ForMember(dest => dest.AllowedScopes, act => act.MapFrom(src =>  string.IsNullOrWhiteSpace(src.AllowedScopes)?null: src.AllowedScopes.Split(SEPARATORS) ))
                .ForMember(dest => dest.RedirectUris, act => act.MapFrom(src => string.IsNullOrWhiteSpace(src.RedirectUris) ? null : src.RedirectUris.Split(SEPARATORS)))
                .ForMember(dest => dest.PostLogoutRedirectUris, act => act.MapFrom(src => string.IsNullOrWhiteSpace(src.PostLogoutRedirectUris) ? null : src.PostLogoutRedirectUris.Split(SEPARATORS)));
        }
    }
}

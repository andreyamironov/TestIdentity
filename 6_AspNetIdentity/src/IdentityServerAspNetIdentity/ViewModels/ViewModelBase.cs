using Microsoft.AspNetCore.Mvc;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class ViewModelBase
    {
        [HiddenInput]
        public string ReturnUrl_VmProperty { get; set; }
    }
}

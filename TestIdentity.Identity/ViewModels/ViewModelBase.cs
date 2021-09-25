using Microsoft.AspNetCore.Mvc;

namespace TestIdentity.Identity.ViewModels
{
    public class ViewModelBase
    {
        [HiddenInput]
        public string ReturnUrl_VmProperty { get; set; }
    }
}

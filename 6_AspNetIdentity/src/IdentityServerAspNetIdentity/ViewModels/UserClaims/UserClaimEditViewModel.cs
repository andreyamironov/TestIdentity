using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class UserClaimEditViewModel : UserClaimViewModel
    {
        public UserClaimEditViewModel()
        {

        }
        public UserClaimEditViewModel(UserClaimViewModel model) : this()
        {
            UserId = model.UserId;
            UserName = model.UserName;
            Type = TypeOriginal = model.Type;
            Value = ValueOriginal = model.Value;
        }

        [HiddenInput]
        public string TypeOriginal { get; set; }
        [HiddenInput]
        public string ValueOriginal { get; set; }
    }
}

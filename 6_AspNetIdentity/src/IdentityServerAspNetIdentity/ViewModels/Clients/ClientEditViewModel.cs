using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class ClientEditViewModel : ClientDetailsViewModel
    {
        [HiddenInput]
        public string ReturnUrl_VmProperty { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList SelectTokenUsages { get; set; }
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList SelectTokenExpirations { get; set; }
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList SelectAccessTokenTypes { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class DropDownActionModel
    {
        public IEnumerable<DropDownActionItemModel> Items { get; set; }
    }

    public class DropDownActionItemModel : MenuItem
    {
        public IDictionary<string, string> RouteData { get; set; }
        //public dynamic RouteData { get; set; }

        public string ReturnUrl { get; set; }
    }
}

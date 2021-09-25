using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class PagerModel
    {
        public string Controller { get; set; }
        public int Page { get; set; }
        public int PageDown { get; set; }
        public int PageUp { get; set; }
        public int ItemsPerPage { get; set; }
        public int[] ItemsPerPageVariant { get; set; }
        public int TotalCount { get; set; }
        public int RangeStart { get; set; }
        public int RangeStop { get; set; }
        public string Search { get; set; }
        public string Tag { get; set; }

    }
}

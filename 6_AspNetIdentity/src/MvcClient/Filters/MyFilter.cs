using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClient.Filters
{
    public class MyFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        readonly string _appPolycy;

        public MyFilter(string appPolicy)
        {
            _appPolycy = appPolicy;
        }
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            ;
            filterContext.Result = new UnauthorizedResult();
        }
    }
}

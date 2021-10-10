using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        readonly string _keyPolicy;
        readonly string _appPolicy;


        public MyFilter(string keyPolicy, string appPolicy)
        {
            _keyPolicy = keyPolicy;
            _appPolicy = appPolicy;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims;
            var claim = claims.Where(c => c.Type == _keyPolicy);
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
    }
}

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

        const char separator = ',';
        public MyFilter(string keyPolicy, string appPolicy)
        {
            _keyPolicy = keyPolicy;
            _appPolicy = appPolicy;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isOk = false;

            var claims = context.HttpContext.User.Claims;
            var claim = claims.Where(c => c.Type == _keyPolicy).FirstOrDefault();
            if(claim !=null)
            {
                if(!string.IsNullOrWhiteSpace(claim.Value))
                {
                    var policies = System.Text.Json.JsonSerializer.Deserialize<string[]>(claim.Value);
                    foreach(var policy in policies)
                    {
                        if (String.Compare(policy, _appPolicy) == 0)
                        {
                            isOk = true;
                            break;
                        }
                    }
                }
            }

            if (!isOk) context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
    }
}

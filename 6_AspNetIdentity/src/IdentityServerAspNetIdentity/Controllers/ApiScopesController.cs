using AMir.Wrapper;
using IdentityServerAspNetIdentity.Core;
using IdentityServerAspNetIdentity.Queries.ApiScopes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ApiScopesController : AppBaseController
    {
        IMediator _mediator;

        public ApiScopesController(IWebHostEnvironment webHostEnvironment, IDiagnosticContext diagnosticContext, IMediator mediator) : base(webHostEnvironment, diagnosticContext)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(string search = null)
        {

            //var tmpId = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ORIGINAL_ID, true);
            var tempDataReturnUrl = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_RETURN_URL);

            IdentityServerAspNetIdentity.Core.HttpParams httpParams;
            if (!string.IsNullOrWhiteSpace(tempDataReturnUrl))
                httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(tempDataReturnUrl);
            else
                httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(HttpContext.Request.QueryString.Value);

            var model = await _mediator.Send(new GetApiScopesPagerListQuery(httpParams));

            return View(model);
        }
    }
}

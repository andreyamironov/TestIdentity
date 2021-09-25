using AMir.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Core;
using IdentityServerAspNetIdentity.Infrastructure;
using IdentityServerAspNetIdentity.Queries.LogEvents;

namespace IdentityServerAspNetIdentity.Controllers
{
    public class LogEventsController : AppBaseController      
    {
        IMediator _mediator;


        public LogEventsController(IWebHostEnvironment webHostEnvironment, IDiagnosticContext diagnosticContext, IMediator mediator) :base(webHostEnvironment,diagnosticContext)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(string search = null)
        {
            var tempDataReturnUrl = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_RETURN_URL);

            IdentityServerAspNetIdentity.Core.HttpParams httpParams;
            if (!string.IsNullOrWhiteSpace(tempDataReturnUrl))
                httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(tempDataReturnUrl);
            else
                httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(HttpContext.Request.QueryString.Value);

            var model = await _mediator.Send(new GetLogEventsPagerListQuery(httpParams));

            return View(model);



            //IdentityServerAspNetIdentity.Core.HttpParams httpParams;
            //httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(HttpContext);
            //var model = _logEventsBroker.Get(httpParams);
            //return View(model);
        }
    }
}

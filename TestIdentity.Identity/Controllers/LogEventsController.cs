using AMir.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Core;
using TestIdentity.Identity.Infrastructure;
using TestIdentity.Identity.Queries.LogEvents;

namespace TestIdentity.Identity.Controllers
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

            TestIdentity.Identity.Core.HttpParams httpParams;
            if (!string.IsNullOrWhiteSpace(tempDataReturnUrl))
                httpParams = TestIdentity.Identity.Core.HttpParams.Get(tempDataReturnUrl);
            else
                httpParams = TestIdentity.Identity.Core.HttpParams.Get(HttpContext.Request.QueryString.Value);

            var model = await _mediator.Send(new GetLogEventsPagerListQuery(httpParams));

            return View(model);
        }
    }
}

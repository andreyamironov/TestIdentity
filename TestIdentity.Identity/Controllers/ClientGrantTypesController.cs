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
using TestIdentity.Identity.Queries.ClientGrantTypes;

namespace TestIdentity.Identity.Controllers
{
    public class ClientGrantTypesController : AppBaseController
    {
        IMediator _mediator;
        public ClientGrantTypesController(IWebHostEnvironment webHostEnvironment, IDiagnosticContext diagnosticContext, IMediator mediator) : base(webHostEnvironment, diagnosticContext)
        {
            _mediator = mediator;
        }

        public IActionResult Index(int id, string returnUrl = null)
        {
             return RedirectToActionPermanent("Details","ClientGrantTypes",new { id = id, returnUrl = returnUrl });
        }

        public async Task<IActionResult> Details(int id, string returnUrl = null)
        {
            var tempDataReturnUrl = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_RETURN_URL);

            TestIdentity.Identity.Core.HttpParams httpParams;

            if (!string.IsNullOrWhiteSpace(tempDataReturnUrl))
                httpParams = TestIdentity.Identity.Core.HttpParams.Get(tempDataReturnUrl);
            else
                httpParams = TestIdentity.Identity.Core.HttpParams.Get(HttpContext);

            var model = await _mediator.Send(new DetailsClientGrantTypesGetQuery(id));

            return View(model);
        }
    }
}

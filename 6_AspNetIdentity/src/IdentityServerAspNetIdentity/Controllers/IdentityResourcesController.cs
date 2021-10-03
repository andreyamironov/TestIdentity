using AMir.Wrapper;
using IdentityServerAspNetIdentity.Core;
using IdentityServerAspNetIdentity.Queries.IdentityResource;
using IdentityServerAspNetIdentity.ViewModels;
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
    public class IdentityResourcesController : AppBaseController
    {
        IMediator _mediator;

        public IdentityResourcesController(IWebHostEnvironment webHostEnvironment, IDiagnosticContext diagnosticContext, IMediator mediator) : base(webHostEnvironment, diagnosticContext)
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

            var model = await _mediator.Send(new GetIdentityResourcesPagerListQuery(httpParams));

            return View(model);
        }
        [HttpGet]
        public IActionResult CreateStart(string returnUrl = null)
        {
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW);
            return RedirectToAction("Create", new { returnUrl = returnUrl });
        }

        [HttpGet]
        public async Task<IActionResult> Create(string returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW))) return RedirectToAction("Index");
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW);
            var model = await _mediator.Send(new CreateIdentityResourceGetQuery(new IdentityResourceCreateViewModel()));
            model.ReturnUrl_VmProperty = returnUrl;
            return View(model);
        }
    }
}

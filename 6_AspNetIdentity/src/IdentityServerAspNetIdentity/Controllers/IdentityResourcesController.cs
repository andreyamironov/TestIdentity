using AMir.Wrapper;
using IdentityServerAspNetIdentity.Commands.IdentityResource;
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

        [HttpGet]
        public async Task<IActionResult> Create(string returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW))) return RedirectToAction("Index");
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW);
            var model = await _mediator.Send(new CreateIdentityResourceGetQuery(new IdentityResourceCreateViewModel()));
            model.ReturnUrl_VmProperty = returnUrl;
            return View(model);
        }
      
        [HttpGet]
        public IActionResult CreateStart(string returnUrl = null)
        {
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW);
            return RedirectToAction("Create", new { returnUrl = returnUrl });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([FromForm()] IdentityResourceCreateViewModel model)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW))) return RedirectToAction("Index");

            try
            {
                if (ModelState.IsValid)
                {
                    HttpParams httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(model.ReturnUrl_VmProperty);
                    var postModel = await _mediator.Send(new CreateIdentityResourcePostCommand(model));
                    httpParams.SelectedId = postModel.Id;

                    TempData.SetValue(KeyWord.KEY_TEMPDATA_INFO, $"IdentityResource '{postModel.Name}' has been CREATED");

                    return RedirectPermanent($"/IdentityResources{httpParams.QueryStringFromProperties}");
                }
                var getModel = await _mediator.Send(new CreateIdentityResourceGetQuery(model));
                TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW);
                return View(getModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [HttpGet]
        public IActionResult EditStart(int id, string returnUrl = null)
        {
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW);
            return RedirectToAction("Edit", new { id = id, returnUrl = returnUrl });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id, string returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW))) return RedirectToAction("Index");

            var model = await _mediator.Send(new EditIdentityResourceGetQuery(id));
            if (model == null) return NotFound();

            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW);
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ORIGINAL_ID, id);

            model.ReturnUrl_VmProperty = returnUrl;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm()] IdentityResourceEditViewModel model)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW))) return RedirectToAction("Index");

            try
            {
                if (ModelState.IsValid)
                {
                    var tmpId = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ORIGINAL_ID);
                    model.Id = int.Parse(tmpId);

                    HttpParams httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(model.ReturnUrl_VmProperty);
                    var editViewModel = await _mediator.Send(new EditIdentityResourcePostCommand(model));
                    httpParams.SelectedId = editViewModel.Id;

                    return RedirectPermanent($"/IdentityResources{httpParams.QueryStringFromProperties}");
                }
                TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW);
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
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
    }
}

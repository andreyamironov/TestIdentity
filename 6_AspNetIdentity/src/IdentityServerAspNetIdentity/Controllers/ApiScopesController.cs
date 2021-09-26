using AMir.Wrapper;
using IdentityServerAspNetIdentity.Commands.ApiScopes;
using IdentityServerAspNetIdentity.Core;
using IdentityServerAspNetIdentity.Queries.ApiScopes;
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
    public class ApiScopesController : AppBaseController
    {
        IMediator _mediator;

        public ApiScopesController(IWebHostEnvironment webHostEnvironment, IDiagnosticContext diagnosticContext, IMediator mediator) : base(webHostEnvironment, diagnosticContext)
        {
            _mediator = mediator;
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
            var model = await _mediator.Send(new CreateApiScopeGetQuery(new ApiScopeCreateViewModel()));
            model.ReturnUrl_VmProperty = returnUrl;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([FromForm()] ApiScopeCreateViewModel model)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW))) return RedirectToAction("Index");

            try
            {
                if (ModelState.IsValid)
                {
                    HttpParams httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(model.ReturnUrl_VmProperty);
                    var postModel = await _mediator.Send(new CreateApiScopesPostCommand(model));
                    httpParams.SelectedId = postModel.Id;

                    TempData.SetValue(KeyWord.KEY_TEMPDATA_INFO, $"ApiScope '{postModel.Name}' has been CREATED");

                    return RedirectPermanent($"/ApiScopes{httpParams.QueryStringFromProperties}");
                }
                var getModel = await _mediator.Send(new CreateApiScopeGetQuery(model));
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

            var model = await _mediator.Send(new EditApiScopeGetQuery(id));
            if (model == null) return NotFound();

            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW);
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ORIGINAL_ID, id);

            model.ReturnUrl_VmProperty = returnUrl;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm()] ApiScopeEditViewModel model)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW))) return RedirectToAction("Index");

            try
            {
                if (ModelState.IsValid)
                {
                    var tmpId = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ORIGINAL_ID);
                    model.Id = int.Parse(tmpId);

                    HttpParams httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(model.ReturnUrl_VmProperty);
                    var editViewModel = await _mediator.Send(new EditApiScopePostCommand(model));
                    httpParams.SelectedId = editViewModel.Id;

                    return RedirectPermanent($"/ApiScopes{httpParams.QueryStringFromProperties}");
                }
                TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW);
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [HttpGet]
        public IActionResult DeleteStart(int id, string returnUrl = null)
        {
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW);
            return RedirectToAction("Delete", new { id = id, returnUrl = returnUrl });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id, string returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW))) return RedirectToAction("Index");


            var model = await _mediator.Send(new EditApiScopeGetQuery(id));
            if (model == null) return NotFound();

            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW);
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ORIGINAL_ID, $"id:{model.Id};name:{model.Name}");


            model.ReturnUrl_VmProperty = returnUrl;
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm([FromForm()] ApiScopeEditViewModel model)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW))) return RedirectToAction("Index");

            try
            {
                if (ModelState.IsValid)
                {
                    //var tmpId = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ORIGINAL_ID);
                    //model.Id = int.Parse(tmpId);


                    string tmpId = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ORIGINAL_ID);
                    IDictionary<string, string> tmpKeyValue = tmpId.SplitToKeyValue();

                    string idFromTempdate = tmpKeyValue["id"];
                    int idOriginal;
                    int.TryParse(idFromTempdate, out idOriginal);

                    string nameFromTempdate = tmpKeyValue["name"];
                    model.Id = idOriginal;

                    HttpParams httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(model.ReturnUrl_VmProperty);
                    var deleteResult = await _mediator.Send(new DeleteApiScopePostCommand(model.Id));

                    if (deleteResult) TempData.SetValue(KeyWord.KEY_TEMPDATA_INFO, $"ApiScope '{nameFromTempdate}' has been deleted");
                    else TempData.SetValue(KeyWord.KEY_TEMPDATA_INFO, $"ApiScope '{nameFromTempdate}' deletion ERROR");

                    return RedirectPermanent($"/ApiScopes{httpParams.QueryStringFromProperties}");
                }
                TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW);
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, string returnUrl = null)
        {
            TempData.SetValue(KeyWord.KEY_TEMPDATA_RETURN_URL, returnUrl);

            var model = await _mediator.Send(new DetailsApiScopeGetQuery(id));
            if (model == null) return NotFound();
            //model.ReturnUrl_VmProperty = returnUrl;
            return View(model);
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

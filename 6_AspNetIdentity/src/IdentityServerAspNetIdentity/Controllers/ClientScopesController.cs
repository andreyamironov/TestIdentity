using AMir.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Commands.Clients;
using IdentityServerAspNetIdentity.Commands.ClientScopes;
using IdentityServerAspNetIdentity.Core;
using IdentityServerAspNetIdentity.Queries.ClientScopes;
using IdentityServerAspNetIdentity.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace IdentityServerAspNetIdentity.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ClientScopesController : AppBaseController
    {
        IMediator _mediator;

        public ClientScopesController(IWebHostEnvironment webHostEnvironment, IDiagnosticContext diagnosticContext, IMediator mediator) : base(webHostEnvironment, diagnosticContext)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult CreateStart(int id,string returnUrl = null)
        {
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW);
            return RedirectToAction("Create", new {id=id, returnUrl = returnUrl });
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id, string returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW))) return RedirectToAction("Index");
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW);
            var model = await _mediator.Send(new CreateClientScopetGetQuery(id));
            model.ReturnUrl_VmProperty = returnUrl;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([FromForm()] ClientScopeCreateViewModel model)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW))) return RedirectToAction("Index");

            try
            {
                if (ModelState.IsValid)
                {
                    HttpParams httpParams   = IdentityServerAspNetIdentity.Core.HttpParams.Get(model.ReturnUrl_VmProperty);
                    var postModel           = await _mediator.Send(new CreateClientScopePostCommand(model));
                    httpParams.SelectedId   = postModel.Id;

                    TempData.SetValue(KeyWord.KEY_TEMPDATA_INFO, $"Scope '{model.Scope}' for client '{model.ClientName}' has been CREATED");

                    return RedirectPermanent(model.ReturnUrl_VmProperty);
                    //return RedirectPermanent($"/ClientScopes{httpParams.QueryStringFromProperties}");

                }
                var getModel = await _mediator.Send(new CreateClientScopetGetQuery(model.ClientId));
                TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW);
                return View(getModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        [HttpGet]
        public IActionResult EditStart(int id, string scope, string returnUrl = null)
        {
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW);
            return RedirectToAction("Edit", new { id = id, scope= scope, returnUrl = returnUrl });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id, string scope, string returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW))) return RedirectToAction("Index",new { id=id});

            var model = await _mediator.Send(new EditClientScopeGetQuery(id,scope));
            if (model == null) return NotFound();

            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW);
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ORIGINAL_ID, $"clientid:{id};scope:{scope}");

            model.ReturnUrl_VmProperty = returnUrl;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm()] ClientScopeEditViewModel model)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW))) return RedirectToAction("Index");

            try
            {
                if (ModelState.IsValid)
                {
                    string tmpId = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ORIGINAL_ID);
                    IDictionary<string,string> tmpKeyValue = tmpId.SplitToKeyValue();

                    string clientIdFromTempdate = tmpKeyValue["clientid"];
                    int clientIdOriginal;
                    int.TryParse(clientIdFromTempdate, out clientIdOriginal);

                    model.ClientId          = clientIdOriginal;
                    model.ScopeOriginal     = tmpKeyValue["scope"];

                    HttpParams httpParams   = IdentityServerAspNetIdentity.Core.HttpParams.Get(model.ReturnUrl_VmProperty);
                    var editViewModel       = await _mediator.Send(new EditClientScopePostCommand(model));
                    //httpParams.SelectedId = editViewModel.Id;

                    //return RedirectPermanent($"/ClientScopes{httpParams.QueryStringFromProperties}");
                    return RedirectPermanent(model.ReturnUrl_VmProperty);
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
        public IActionResult DeleteStart(int id,string scope, string returnUrl = null)
        {
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW);
            return RedirectToAction("Delete", new { id = id, scope = scope, returnUrl = returnUrl });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id, string scope, string returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW))) return RedirectToAction("Index");

            var model = await _mediator.Send(new EditClientScopeGetQuery(id,scope));
            if (model == null) return NotFound();

            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW);
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ORIGINAL_ID, $"clientid:{id};scope:{scope}");

            model.ReturnUrl_VmProperty = returnUrl;
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm([FromForm()] ClientScopeEditViewModel model)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW))) return RedirectToAction("Index");

            try
            {
                if (ModelState.IsValid)
                {
                    string tmpId = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ORIGINAL_ID);
                    IDictionary<string, string> tmpKeyValue = tmpId.SplitToKeyValue();

                    string clientIdFromTempdate = tmpKeyValue["clientid"];
                    int clientIdOriginal;
                    int.TryParse(clientIdFromTempdate, out clientIdOriginal);

                    model.ClientId  = clientIdOriginal;
                    model.Scope     = tmpKeyValue["scope"];

                    HttpParams httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(model.ReturnUrl_VmProperty);
                    var deleteResult = await _mediator.Send(new DeleteClientScopePostCommand(model));

                    if (deleteResult) TempData.SetValue(KeyWord.KEY_TEMPDATA_INFO, $"Scope '{model.Scope}' by User '{model.ClientName}' has been deleted");
                    else TempData.SetValue(KeyWord.KEY_TEMPDATA_INFO, $"Scope '{model.Scope}' by User '{model.ClientName}' deletion ERROR");

                    return RedirectPermanent(model.ReturnUrl_VmProperty);

                    //return RedirectPermanent($"/Clients{httpParams.QueryStringFromProperties}");
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
        public async Task<IActionResult> Index(int id, string search = null)
        {
            //if (string.IsNullOrWhiteSpace(id)) return NotFound();
            var tempDataReturnUrl = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_RETURN_URL);

            IdentityServerAspNetIdentity.Core.HttpParams httpParams;

            if (!string.IsNullOrWhiteSpace(tempDataReturnUrl))
                httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(tempDataReturnUrl);
            else
                httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(HttpContext);

            var model = await _mediator.Send(new GetClientsScopesPagerListQuery(httpParams,id));

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(string clientid, string scope, string returnUrl = null)
        {
            TempData.SetValue(KeyWord.KEY_TEMPDATA_RETURN_URL, returnUrl);
            return View(new ClientScopeViewModel() { ClientName = clientid, Scope = scope });
        }
    }
}

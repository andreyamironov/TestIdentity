using AMir.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Commands.Clients;
using TestIdentity.Identity.Core;
using TestIdentity.Identity.Infrastructure;
using TestIdentity.Identity.Queries.Clients;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class ClientsController : AppBaseController
    {
        IMediator _mediator;
        public ClientsController(IWebHostEnvironment webHostEnvironment, IDiagnosticContext diagnosticContext, IMediator mediator) : base(webHostEnvironment, diagnosticContext)
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
            var model = await _mediator.Send(new CreateClientGetQuery(new ClientCreateViewModel()));
            model.ReturnUrl_VmProperty = returnUrl;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([FromForm()] ClientCreateViewModel model)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW))) return RedirectToAction("Index");

            try
            {
                if (ModelState.IsValid)
                {
                    HttpParams httpParams = TestIdentity.Identity.Core.HttpParams.Get(model.ReturnUrl_VmProperty);
                    var postModel = await _mediator.Send(new CreateClientPostCommand(model));
                    httpParams.SelectedId = postModel.Id;

                    TempData.SetValue(KeyWord.KEY_TEMPDATA_INFO, $"User '{model.ClientId}' has been CREATED");

                    return RedirectPermanent($"/Clients{httpParams.QueryStringFromProperties}");
                }
                var getModel = await _mediator.Send(new CreateClientGetQuery(model));
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

            var model = await _mediator.Send(new EditClientGetQuery(id));
            if (model == null) return NotFound();

            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW);
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ORIGINAL_ID, id);

            model.ReturnUrl_VmProperty = returnUrl;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm()] ClientEditViewModel model)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW))) return RedirectToAction("Index");

            try
            {
                if (ModelState.IsValid)
                {
                    var tmpId = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ORIGINAL_ID);
                    model.Id = int.Parse(tmpId);

                    HttpParams httpParams = TestIdentity.Identity.Core.HttpParams.Get(model.ReturnUrl_VmProperty);
                    var editViewModel = await _mediator.Send(new EditClientPostCommand(model));
                    httpParams.SelectedId = editViewModel.Id;

                    return RedirectPermanent($"/Clients{httpParams.QueryStringFromProperties}");
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


            var model = await _mediator.Send(new EditClientGetQuery(id));
            if (model == null) return NotFound();

            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW);
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ORIGINAL_ID, id);

            model.ReturnUrl_VmProperty = returnUrl;
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm([FromForm()] ClientEditViewModel model)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW))) return RedirectToAction("Index");

            try
            {
                if (ModelState.IsValid)
                {
                    var tmpId = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ORIGINAL_ID);
                    model.Id = int.Parse(tmpId);

                    HttpParams httpParams       = TestIdentity.Identity.Core.HttpParams.Get(model.ReturnUrl_VmProperty);
                    var deleteResult            = await _mediator.Send(new DeleteClientPostCommand(model.Id));

                    if(deleteResult) TempData.SetValue(KeyWord.KEY_TEMPDATA_INFO, $"User '{model.ClientId}' has been deleted");
                    else TempData.SetValue(KeyWord.KEY_TEMPDATA_INFO, $"User '{model.ClientId}' deletion ERROR");

                    return RedirectPermanent($"/Clients{httpParams.QueryStringFromProperties}");
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

            var model = await _mediator.Send(new DetailsClientGetQuery(id));
            if (model == null) return NotFound();
            //model.ReturnUrl_VmProperty = returnUrl;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search = null)
        {
            //var tmpId = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ORIGINAL_ID, true);
            var tempDataReturnUrl = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_RETURN_URL);

            TestIdentity.Identity.Core.HttpParams httpParams;
            if (!string.IsNullOrWhiteSpace(tempDataReturnUrl))
                httpParams = TestIdentity.Identity.Core.HttpParams.Get(tempDataReturnUrl);
            else
                httpParams = TestIdentity.Identity.Core.HttpParams.Get(HttpContext.Request.QueryString.Value);

            var model = await _mediator.Send(new GetClientsPagerListQuery(httpParams));

            return View(model);
        }
    }
}

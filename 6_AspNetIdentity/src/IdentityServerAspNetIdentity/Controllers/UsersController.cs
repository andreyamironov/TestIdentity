using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.ViewModels;
using AMir.Wrapper;
using IdentityServerAspNetIdentity.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using IdentityServerAspNetIdentity.Core;
using IdentityServerAspNetIdentity.Models;
using MediatR;
using IdentityServerAspNetIdentity.Queries.Users;
using IdentityServerAspNetIdentity.Commands.Users;

namespace IdentityServerAspNetIdentity.Controllers
{

   [Authorize(Roles = "Administrator")]
    public class UsersController : AppBaseController
    {
        IMediator _mediator;


        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UsersBroker _usersBroker;
        public UsersController(
            IWebHostEnvironment webHostEnvironment
            ,ApplicationDbContext applicationDbContext 
            ,UserManager<ApplicationUser> userManager
            ,UsersBroker usersBroker
            ,IDiagnosticContext diagnosticContext
            ,IMediator mediator):base(webHostEnvironment,diagnosticContext)
        {
            _applicationDbContext   = applicationDbContext;
            _userManager            = userManager;
            _usersBroker            = usersBroker;
            _mediator               = mediator;
        }

        public async Task<IActionResult> Index(string search = null)
        {
            //var tmpId = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ORIGINAL_ID,true);
            //var tempDataReturnUrl = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_RETURN_URL);

            //IdentityServerAspNetIdentity.Core.HttpParams httpParams;
            //if (!string.IsNullOrWhiteSpace(tempDataReturnUrl)) 
            //    httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(tempDataReturnUrl); 
            //else 
            //    httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(HttpContext);

            //var model = _usersBroker.Get(httpParams,tmpId);
            ////_diagnosticContext.Set("Users_GET", User.Identity.Name);
            //return View(model);


            //var tmpId = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ORIGINAL_ID, true);
            var tempDataReturnUrl = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_RETURN_URL);

            IdentityServerAspNetIdentity.Core.HttpParams httpParams;
            if (!string.IsNullOrWhiteSpace(tempDataReturnUrl))
                httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(tempDataReturnUrl);
            else
                httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(HttpContext.Request.QueryString.Value);

            var model = await _mediator.Send(new GetUsersPagerListQuery(httpParams));

            return View(model);

        }

        [HttpGet]
        public  async Task<IActionResult> Details(string id,string returnUrl = null)
        {
            //TempData.SetValue(KeyWord.KEY_TEMPDATA_RETURN_URL, returnUrl);
            //var model = _userManager.FindByIdAsync(id);
            //return View(model.Result);


            TempData.SetValue(KeyWord.KEY_TEMPDATA_RETURN_URL, returnUrl);

            var model = await _mediator.Send(new DetailsUserGetQuery(new Guid(id)));
            if (model == null) return NotFound();
            //model.ReturnUrl_VmProperty = returnUrl;
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
            var model = await _mediator.Send(new CreateUserGetQuery(new UserCreateViewModel()));
            model.ReturnUrl_VmProperty = returnUrl;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([FromForm()] UserCreateViewModel model)
        {
            if (string.IsNullOrWhiteSpace(TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW))) return RedirectToAction("Index");

            try
            {
                if (ModelState.IsValid)
                {
                    HttpParams httpParams = IdentityServerAspNetIdentity.Core.HttpParams.Get(model.ReturnUrl_VmProperty);
                    var result = await _mediator.Send(new CreateUserPostCommand(model));
                    if (result.Result.Succeeded == true)
                    {
                        httpParams.SelectedId = result.Id;
                        TempData.SetValue(KeyWord.KEY_TEMPDATA_INFO, $"User '{result.Email}' has been CREATED");
                        return RedirectPermanent($"/Users{httpParams.QueryStringFromProperties}");
                    }

                    StringBuilder sb = new StringBuilder();
                    foreach (var er in result.Result.Errors)
                    {
                        sb.AppendLine(er.Description);
                    }

                    ViewBag.ERR = sb.ToString();                  
                }
                var getModel = await _mediator.Send(new CreateUserGetQuery(model));
                TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_POST_ALLOW);
                return View(getModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(string id, string returnUrl = null)
        {

            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            TempData.SetValue(KeyWord.KEY_TEMPDATA_RETURN_URL, returnUrl);
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ORIGINAL_ID, id);

            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(
                new UserEditViewModel()
                {
                    Id=user.Id.ToString(),
                    EMail = user.Email,
                    EmailConfirmed = user.EmailConfirmed
                }
                );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var returnUrl = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_RETURN_URL);
                var tmpId = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ORIGINAL_ID);

                model.OriginalId = tmpId;

                var task = _usersBroker.Update(model);

                if (task.Result.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(returnUrl))
                        return RedirectPermanent(returnUrl);
                    else return await Index();
                }
                else
                {
                    foreach (var error in task.Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    TempData.SetValue(KeyWord.KEY_TEMPDATA_RETURN_URL, returnUrl);
                    TempData.SetValue(KeyWord.KEY_TEMPDATA_ORIGINAL_ID, tmpId);
                }              
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);         
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var roleAdmin = _applicationDbContext.Roles.Where(r => r.Name.CompareTo(
                Enum.GetName(typeof(Core.RoleRight), Core.RoleRight.Administrator)) == 0).FirstOrDefault();

            if (roleAdmin == null) throw new AMir.Exception.AdminRoleNotFoundException();

            var findAdmin = _applicationDbContext.UserRoles.Where(u => u.UserId.ToString() != id && u.RoleId == roleAdmin.Id).FirstOrDefault();
            if (findAdmin != null)
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData.SetValue(KeyWord.KEY_TEMPDATA_INFO, "Пользователь удалён");
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var error in result.Errors)
                    {
                        sb.AppendLine(error.Description);
                    }
                    TempData.SetValue(KeyWord.KEY_TEMPDATA_INFO, sb.ToString());
                }
            }
            else
            {
                TempData.SetValue(KeyWord.KEY_TEMPDATA_INFO, "Должен оставаться хотя бы один администратор системы");
            }         
            return RedirectToAction(nameof(Index));
        }
    }   
}

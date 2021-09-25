using AMir.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TestIdentity.Identity.Core;
using TestIdentity.Identity.Data;
using TestIdentity.Identity.Infrastructure;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ClaimsController : AppBaseController
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserClaimsBroker _userClaimsBroker;

        public ClaimsController(IWebHostEnvironment webHostEnvironment, IDiagnosticContext diagnosticContext
            , UserClaimsBroker userClaimsBroker
            ):base(webHostEnvironment,diagnosticContext)
        {
            _userClaimsBroker = userClaimsBroker;
        }

        public IActionResult Index(string id = null, string search = null)
        {
            var tempDataReturnUrl = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_RETURN_URL);

            TestIdentity.Identity.Core.HttpParams httpParams;

            if (!string.IsNullOrWhiteSpace(tempDataReturnUrl))
                httpParams = TestIdentity.Identity.Core.HttpParams.Get(tempDataReturnUrl);
            else
                httpParams = TestIdentity.Identity.Core.HttpParams.Get(HttpContext);

            if (string.IsNullOrWhiteSpace(id)) return View("Empty");
            var model = _userClaimsBroker.Get(httpParams, id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create(string id = null, string returnUrl = null)
        {
            TempData.SetValue(KeyWord.KEY_TEMPDATA_RETURN_URL, returnUrl);

            try
            {
                var model = _userClaimsBroker.CreateGet(id);
                return View(model);
            }
            catch(AMir.Exception.EntityNotFoundException ex)
            {
                TempData.SetValue(KeyWord.KEY_TEMPDATA_INFO, ex.Message);
            }

            return Redirect(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UserId,Type,Value")] UserClaimViewModel model)
        {
            var returnUrl = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_RETURN_URL);

            try
            {
                if (ModelState.IsValid)
                {
                    _userClaimsBroker.Create(model);
                    TempData.SetValue(KeyWord.KEY_TEMPDATA_INFO, $"User {model.UserName} got the claim, type: {model.Type} value: {model.Value}");
                    return Redirect(returnUrl);
                }
                return View(model);
            }
            catch(AMir.Exception.EntityDuplicateException ex)
            {
                ViewBag.ERR = ex.Message;
                TempData.SetValue(KeyWord.KEY_TEMPDATA_RETURN_URL, returnUrl);
                return View(model);
            }
            catch (AMir.Exception.PairKeyValueIsNullOrEmptyException ex)
            {
                ViewBag.ERR = ex.Message;
                TempData.SetValue(KeyWord.KEY_TEMPDATA_RETURN_URL, returnUrl);
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        [HttpGet]
        public IActionResult Edit(string userId, string type, string value, string returnUrl = null)
        {
            if (!string.IsNullOrWhiteSpace(userId)
                && !string.IsNullOrWhiteSpace(type)
                && !string.IsNullOrWhiteSpace(value))
            {
                UserClaimViewModel find = _userClaimsBroker.Get(userId, type, value);

                if (find == null) return NotFound();

                TempData.SetValue(KeyWord.KEY_TEMPDATA_RETURN_URL, returnUrl);
          
                var tmpOrigin = new UserClaimViewModel(){ UserId = userId, Type = type, Value = value };
                string tmpOriginSerialize = JsonSerializer.Serialize(tmpOrigin);
                TempData.SetValue(KeyWord.KEY_TEMPDATA_ORIGINAL_OBJECT, tmpOriginSerialize);

                var model = new UserClaimEditViewModel(find);
                return View(model);
            }
            else
            {
                return Index(userId);
            }
        }
       
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("UserId,Type,Value,TypeOriginal,ValueOriginal")] UserClaimEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string tmpOriginSerialize = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_ORIGINAL_OBJECT);
                    UserClaimViewModel tmpOriginal = JsonSerializer.Deserialize(tmpOriginSerialize, typeof(UserClaimViewModel)) as UserClaimViewModel;

                    var returnUrl = TempData.GetStringOrEmpty(KeyWord.KEY_TEMPDATA_RETURN_URL);
                    if (tmpOriginal == null) return RedirectToAction(nameof(Index),"Claims",new { id = model.UserId });

                    if (model.UserId != tmpOriginal.UserId
                        || model.TypeOriginal != tmpOriginal.Type
                        || model.ValueOriginal != tmpOriginal.Value) throw new AMir.Exception.PairKeyValueIsNullOrEmptyException();

                    _userClaimsBroker.Update(model);

                    return Redirect(returnUrl);               
                }
                catch (Exception ex)
                {
                    ViewBag.ERR = ex.ToString();
                }
            }
            return View(model);
        }


        public IActionResult Delete(string userid, string type, string value, string returnUrl = null)
        {
            var model = _userClaimsBroker.Get(userid, type, value);
            TempData.SetValue(Core.KeyWord.KEY_TEMPDATA_RETURN_URL, returnUrl);
            return View(model);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAsync(string userid, string type, string value)
        {
            string returnUrl = TempData.GetStringOrEmpty(Core.KeyWord.KEY_TEMPDATA_RETURN_URL);

            if (!string.IsNullOrWhiteSpace(userid) || !string.IsNullOrWhiteSpace(type))
            {
                var find = _applicationDbContext.UserClaims.Where(
                    f => f.UserId == new Guid(userid)
                    && f.ClaimType == type
                    && f.ClaimValue == value);

                if (find != null)
                {
                    _applicationDbContext.RemoveRange(find);
                    //TODO
                    var result = await _applicationDbContext.SaveChangesAsync();
                    if (!string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

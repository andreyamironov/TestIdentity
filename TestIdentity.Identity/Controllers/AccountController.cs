using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Models;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Controllers
{
    [Authorize]
    public class AccountController : AppBaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(IWebHostEnvironment webHostEnvironment, IDiagnosticContext diagnosticContext, 
            SignInManager<ApplicationUser> signInManager
            ,UserManager<ApplicationUser> userManager):base(webHostEnvironment,diagnosticContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;         
        }
     
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {                
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                            await _signInManager.PasswordSignInAsync(user, model.Password, false, true);

                    if (result.Succeeded)
                    {
                        //_logger.LogInformation("User logged in.");
                        user.AccessFailedCount = 0;
                        user.LockoutEnd = null;
                        await _userManager.UpdateAsync(user);

                        _diagnosticContext.Set("Login_Succeeded", user.UserName);

                        return LocalRedirect(returnUrl);
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        return Lockout();
                    }
                    else
                    {
                        user.AccessFailedCount++;
                        await _userManager.UpdateAsync(user);

                        _diagnosticContext.Set("Login_Failed", $"{model.Email} with :{model.Password}");

                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }

                }
                _diagnosticContext.Set("Invalid_User", $"{model.Email} with :{model.Password}");

                ModelState.AddModelError(nameof(LoginModel.Email),
                    "Invalid user or password");
            }
            return View();
        }


        public IActionResult Manage()
        {
            AccountDetailsViewModel model = new AccountDetailsViewModel();

            model.UserName = User.Identity.Name;
            List<KeyValuePair<string, string>> claims = new List<KeyValuePair<string, string>>();

            foreach(var c in User.Claims.OrderBy(c=>c.Type))
            {
                claims.Add(new KeyValuePair<string, string>(c.Type, c.Value));
            }

            model.Claims = claims;
            return View(model);
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Lockout()
        {
            return View("Lockout");
        }
    }
}

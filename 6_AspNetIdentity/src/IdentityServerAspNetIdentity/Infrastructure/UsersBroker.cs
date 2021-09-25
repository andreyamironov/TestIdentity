using AMir.Wrapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Models;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Infrastructure
{
    public class UsersBroker
    {
        IWebHostEnvironment _appEnvironment;
        UserManager<ApplicationUser> _userManager;
        IPasswordValidator<ApplicationUser> _passwordValidator;
        IPasswordHasher<ApplicationUser> _passwordHasher;
        public UsersBroker() { }

        public UsersBroker(IWebHostEnvironment appEnvironment
            ,UserManager<ApplicationUser> userManager
            ,IPasswordValidator<ApplicationUser> passwordValidator
            ,IPasswordHasher<ApplicationUser> passwordHasher)
        {
            this._appEnvironment    = appEnvironment;
            this._userManager       = userManager;
            this._passwordValidator = passwordValidator;
            this._passwordHasher    = passwordHasher;
        }
        public UsersViewModel Get(IdentityServerAspNetIdentity.Core.HttpParams httpParams,string selectedId = "")
        {
            UsersViewModel pagerListModel = new UsersViewModel();
            pagerListModel.TotalCount   = string.IsNullOrWhiteSpace(httpParams.Search) 
                ? _userManager.Users.Count() 
                : _userManager.Users.Count(u=>u.Email.IndexOf(httpParams.Search) > -1);
            pagerListModel.ItemsPerPage = (int)(httpParams.Count > 0 ? httpParams.Count : 10);

            if (pagerListModel.TotalCount <= pagerListModel.ItemsPerPage)
            {
                pagerListModel.Page = 1;
            }
            else if(string.IsNullOrWhiteSpace(selectedId))
            {
                pagerListModel.Page = (int)(httpParams.Page > 0 ? httpParams.Page : 1);
            }
            else
            {
                try
                {
                    var findUser = _userManager.FindByIdAsync(selectedId);
                    if (findUser.Result != null)
                    {
                        int indexUser = _userManager.Users.IndexOf(findUser.Result);
                        pagerListModel.Page = (indexUser / pagerListModel.ItemsPerPage) + 1;
                    }
                }
                catch
                {
                    pagerListModel.Page = 1;
                }
            }

            pagerListModel.Search       = httpParams.Search;
            pagerListModel.QueryString     = httpParams.QueryString;

            var users = _userManager.Users
                .Where(u => string.IsNullOrWhiteSpace(pagerListModel.Search) ? true : u.UserName.Contains(pagerListModel.Search))
                .Skip((pagerListModel.Page - 1) * pagerListModel.ItemsPerPage)
                .Take(pagerListModel.ItemsPerPage);

            if (users != null)
            {
                pagerListModel.Items = users;
            }

            return pagerListModel;
        }

        public async Task<IdentityResult> Update(UserEditViewModel model)
        {
            IdentityResult passwordValidatorResult      = IdentityResult.Success;
            IEnumerable<IdentityError> errorsPassword   = new IdentityError[0];

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user != null && model.OriginalId.CompareTo(model.Id) == 0)
            {
                user.UserName = user.Email = model.EMail;
                user.EmailConfirmed = model.EmailConfirmed;

                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    passwordValidatorResult = await _passwordValidator.ValidateAsync(_userManager, user, model.Password);
                    if (passwordValidatorResult.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
                    }
                    else
                    {
                        errorsPassword =  passwordValidatorResult.Errors;
                    }
                }

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded && passwordValidatorResult.Succeeded)
                {
                    return IdentityResult.Success;
                }
                else
                {
                    List<IdentityError> errorList = new List<IdentityError>();                  
                    errorList.AddRange(result.Errors);                  
                    errorList.AddRange(errorsPassword);
                    return IdentityResult.Failed(errorList.ToArray());
                }
            }
            else
            {
                return IdentityResult.Failed(new IdentityError() { Code = "500", Description = "User not found"});
            }
        }
    }   
}

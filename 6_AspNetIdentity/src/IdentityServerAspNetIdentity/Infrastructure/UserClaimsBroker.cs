using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.Models;
using IdentityServerAspNetIdentity.ViewModels;
using AMir.Wrapper;


namespace IdentityServerAspNetIdentity.Infrastructure
{
    public class UserClaimsBroker
    {
        IWebHostEnvironment _appEnvironment;
        ApplicationDbContext _applicationDbContext;
        public UserClaimsBroker() { }

        public UserClaimsBroker(IWebHostEnvironment appEnvironment, ApplicationDbContext applicationDbContext)
        {
            this._appEnvironment = appEnvironment;
            this._applicationDbContext = applicationDbContext;
        }

        public UserClaimsViewModel Get(IdentityServerAspNetIdentity.Core.HttpParams httpParams, string userId, string selectedId = null)
        {
            UserClaimsViewModel userClaimsViewModel = new UserClaimsViewModel();
            List<UserClaimViewModel> claimsModel        = new List<UserClaimViewModel>();
            ApplicationUser applicationUser         = _applicationDbContext.Users.FirstOrDefault(u => u.Id == new Guid(userId));

            userClaimsViewModel.TotalCount = string.IsNullOrWhiteSpace(httpParams.Search)
                ? _applicationDbContext.UserClaims.Count(u => u.UserId == new Guid(userId))
                : _applicationDbContext.UserClaims.Count(u => u.UserId == new Guid(userId) && u.ClaimType.IndexOf(httpParams.Search) > -1);
            userClaimsViewModel.ItemsPerPage = (int)(httpParams.Count > 0 ? httpParams.Count : 10);

            if (userClaimsViewModel.TotalCount <= userClaimsViewModel.ItemsPerPage)
            {
                userClaimsViewModel.Page = 1;
            }
            else if (string.IsNullOrWhiteSpace(selectedId))
            {
                userClaimsViewModel.Page = (int)(httpParams.Page > 0 ? httpParams.Page : 1);
            }
            else
            {
                int tryId = 0;
                if (int.TryParse(selectedId, out tryId))
                {
                    var findUser = _applicationDbContext.UserClaims.FindAsync(tryId);
                    if (findUser.Result != null)
                    {
                        int indexUser = _applicationDbContext.UserClaims.IndexOf(findUser.Result);
                        userClaimsViewModel.Page = (indexUser / userClaimsViewModel.ItemsPerPage) + 1;
                    }
                }
            }

            userClaimsViewModel.Search = httpParams.Search;
            userClaimsViewModel.QueryString = httpParams.QueryString;

            var claimsDb = _applicationDbContext.UserClaims
                .Where(u => string.IsNullOrWhiteSpace(userClaimsViewModel.Search) ? u.UserId == new Guid(userId) : u.UserId == new Guid(userId) && u.ClaimType.Contains(userClaimsViewModel.Search))
                .Skip((userClaimsViewModel.Page - 1) * userClaimsViewModel.ItemsPerPage)
                .Take(userClaimsViewModel.ItemsPerPage).OrderBy(o=>o.ClaimType).ThenBy(o=>o.ClaimValue);

            foreach (var c in claimsDb)
            {
                claimsModel.Add(
                    new UserClaimViewModel()
                    {
                        UserId = c.UserId.ToString(),
                        Type = c.ClaimType,
                        Value = c.ClaimValue
                    }
                    );
            }

            if (applicationUser != null)
            {
                userClaimsViewModel.Informations.SetValue("UserId",applicationUser.Id.ToString());
                userClaimsViewModel.Informations.SetValue("UserName",applicationUser.UserName);
            }

            userClaimsViewModel.Items           = claimsModel;

            return userClaimsViewModel;
        }
        public UserClaimViewModel Get(string userId, string type, string value)
        {

            var userDb = _applicationDbContext.Users.Where(u => u.Id.ToString() == userId).FirstOrDefault();
            var claimeDb = _applicationDbContext.UserClaims.Where(
                u => u.UserId.ToString() == userId 
                && u.ClaimType == type
                 && u.ClaimValue == value).FirstOrDefault();
            if(userDb!=null && claimeDb != null)
            {
                UserClaimViewModel userClaimModel = new UserClaimViewModel();

                userClaimModel.Type = claimeDb.ClaimType;
                userClaimModel.Value = claimeDb.ClaimValue;
                userClaimModel.UserName = userDb?.UserName;
                userClaimModel.UserId = claimeDb.UserId.ToString();
                return userClaimModel;

            }
            return null;
        }


        public UserClaimViewModel CreateGet(string userId)
        {
            var user = _applicationDbContext.Users.Where(u => u.Id.ToString() == userId).FirstOrDefault();
            if (user != null)
            {
                UserClaimViewModel userClaymModel = new UserClaimViewModel()
                {
                    UserId = user.Id.ToString(),
                    UserName = user.Email,
                };
                return userClaymModel;
            }
            else
            {
                throw new AMir.Exception.EntityNotFoundException();
            }
        }
        public int Create(UserClaimViewModel userClaimModel)
        {
            return Create(userClaimModel.UserId, userClaimModel.Type, userClaimModel.Value);
        }
        public int Create(string userId, string type, string value)
        {
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(value)) throw new AMir.Exception.PairKeyValueIsNullOrEmptyException();

            var find = _applicationDbContext.UserClaims.FirstOrDefault(c => c.UserId == new Guid(userId) && c.ClaimType == type&& c.ClaimValue == value);
            if (find != null)
            {
                throw new AMir.Exception.EntityDuplicateException();
            }
            else
            {
                _applicationDbContext.UserClaims.Add(
                        new Microsoft.AspNetCore.Identity.IdentityUserClaim<Guid>()
                        {
                            UserId = new Guid(userId),
                            ClaimType = type,
                            ClaimValue = value
                        }
                    );
               return _applicationDbContext.SaveChanges();
            }
        }


        public int Update(UserClaimEditViewModel userClaimModel)
        {
            return Update(userClaimModel.UserId, userClaimModel.Type, userClaimModel.Value,userClaimModel.TypeOriginal,userClaimModel.ValueOriginal);
        }
        public int Update(string userId, string type, string value,string typeOriginal,string valueOriginal)
        {
            var find = _applicationDbContext.UserClaims.FirstOrDefault(c => c.UserId == new Guid(userId) && c.ClaimType == typeOriginal && c.ClaimValue == valueOriginal);
            if(find != null)
            {
                find.ClaimType = type;
                find.ClaimValue = value;
                _applicationDbContext.Update(find);
                return _applicationDbContext.SaveChanges();
            }
            else
            {
                throw new  AMir.Exception.EntityNotFoundException();
            }
        }
    }
}

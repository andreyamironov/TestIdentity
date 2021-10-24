using AMir.Interface.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Repository.Users
{
    public class UsersWriterRepositoryAsync : IWriterRepositoryAsync<UserCreateResult>
    {
        UserManager<ApplicationUser> _userManager;
        IPasswordValidator<ApplicationUser> _passwordValidator;
        IPasswordHasher<ApplicationUser> _passwordHasher;

        public UsersWriterRepositoryAsync(
            UserManager<ApplicationUser> userManager
            , IPasswordValidator<ApplicationUser> passwordValidator
            , IPasswordHasher<ApplicationUser> passwordHasher)
        {
            this._userManager       = userManager;
            this._passwordValidator = passwordValidator;
            this._passwordHasher    = passwordHasher;
        }

        public async Task<UserCreateResult> Create(UserCreateResult entity)
        {
            try
            {
                var result = await _userManager.CreateAsync(entity, entity.Password);
                entity.Result = result;
                //return entity;
            }
            catch (Exception ex)
            {
                entity.Result = IdentityResult.Failed(new IdentityError() { Description = ex.Message });
            }
            return entity;
        }

        public Task Delete(UserCreateResult entity)
        {
            throw new NotImplementedException();
        }

        public Task<UserCreateResult> Update(UserCreateResult originalEntity, UserCreateResult source = null)
        {
            throw new NotImplementedException();
        }
    }
}

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
    public class UsersWriterRepository : IWriterRepository<ApplicationUser>
    {
        UserManager<ApplicationUser> _userManager;
        IPasswordValidator<ApplicationUser> _passwordValidator;
        IPasswordHasher<ApplicationUser> _passwordHasher;

        public UsersWriterRepository(
            UserManager<ApplicationUser> userManager
            , IPasswordValidator<ApplicationUser> passwordValidator
            , IPasswordHasher<ApplicationUser> passwordHasher)
        {
            this._userManager       = userManager;
            this._passwordValidator = passwordValidator;
            this._passwordHasher    = passwordHasher;
        }

        public ApplicationUser Create(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Update(ApplicationUser originalEntity, ApplicationUser source = null)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using TestIdentity.Identity.Data;
using TestIdentity.Identity.Models;
using TestIdentity.Identity.Core;
using Microsoft.Extensions.Configuration;

namespace TestIdentity.Identity.Infrastructure
{
    public static class DataInializer
    {
        public static async Task InitializeAcync(IServiceProvider serviseProvider)
        {
            var scope = serviseProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var isExist = context.GetService<IDatabaseCreator>() is RelationalDatabaseCreator databaseCreator
                && await databaseCreator.ExistsAsync();

            if (isExist) return;

            await context.Database.MigrateAsync();

            scope = serviseProvider.CreateScope();
            using var roleManager = scope.ServiceProvider.GetService<Microsoft.AspNetCore.Identity.RoleManager<ApplicationRole>>();
            foreach (RoleRight roleRight in Enum.GetValues(typeof(RoleRight)))
            {
                await roleManager.CreateAsync(
                    new ApplicationRole(Enum.GetName(typeof(RoleRight), roleRight))
                    );
            }

            using var userManager = scope.ServiceProvider.GetService<Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>>();


            var config = scope.ServiceProvider.GetService<IConfiguration>();
            var user = new ApplicationUser()
            {
                UserName = config.GetSection("DefaultUser:EMail")?.Value,
                Email = config.GetSection("DefaultUser:EMail")?.Value,
                EmailConfirmed=true
            };
            
            await userManager.CreateAsync(user, config.GetSection("DefaultUser:Password")?.Value);
            await userManager.AddToRoleAsync(user, Enum.GetName(typeof(RoleRight), RoleRight.Administrator));         
        }
    }
}

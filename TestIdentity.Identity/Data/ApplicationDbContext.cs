using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Models;

namespace TestIdentity.Identity.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        const string DefaultSchemeDataBase = "usr";
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(DefaultSchemeDataBase);
        }
    }
}

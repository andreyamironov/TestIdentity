// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using AMir.Interface.Data;
using AutoMapper;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
//using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
//using IdentityServer4.Models;
using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.Infrastructure;
using IdentityServerAspNetIdentity.Mapping;
using IdentityServerAspNetIdentity.Models;
using IdentityServerAspNetIdentity.Repository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Reflection;

namespace IdentityServerAspNetIdentity
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            var mapperConfig = MapperRegistration.GetMapperConfiguration();
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMediatR(typeof(Startup).Assembly);

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationsAssemblyIdentityServer = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDbContext<ApplicationEventDbContext>(options =>
               options.UseSqlServer(connectionString));


            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
                //.AddInMemoryIdentityResources(Config.IdentityResources)
                //.AddInMemoryApiScopes(Config.ApiScopes)
                //.AddInMemoryClients(Config.Clients)
                //.AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssemblyIdentityServer));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssemblyIdentityServer));
                })
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<ProfileService>();
            ;

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            //services.AddAuthentication()
            //    .AddGoogle(options =>
            //    {
            //        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

            //        // register your IdentityServer with Google at https://console.developers.google.com
            //        // enable the Google+ API
            //        // set the redirect URI to https://localhost:5001/signin-google
            //        options.ClientId = "copy client ID from Google here";
            //        options.ClientSecret = "copy client secret from Google here";
            //    });


            services.AddTransient(typeof(UsersBroker));
            services.AddTransient(typeof(UserClaimsBroker));
            services.AddTransient(typeof(IReaderRepository<Client, int>), typeof(Repository.Clients.ClientsReaderRepository));
            services.AddTransient(typeof(IReaderByOwnerRepository<int, ClientScope, int>), typeof(Repository.ClientScopes.ClientScopesReaderRepository));
            services.AddTransient(typeof(IReaderByOwnerRepository<int, ClientGrantType, int>), typeof(Repository.ClientGrantTypes.ClientGrantTypesReaderRepository));

            services.AddTransient(typeof(IReaderRepository<ApiScope, int>), typeof(Repository.ApiScopes.ApiScopesReaderRepository));
            services.AddTransient(typeof(IWriterRepository<ApiScope>), typeof(Repository.ApiScopes.ApiScopesWriterRepository));

            services.AddTransient(typeof(IWriterRepository<Client>), typeof(Repository.Clients.ClientsWriterRepository));
            services.AddTransient(typeof(IWriterRepository<ClientScope>), typeof(Repository.ClientScopes.ClientScopesWriterRepository));

            services.AddTransient(typeof(IReaderRepository<LogEvent, int>), typeof(LogEventsReaderRepository));
        }

        public void Configure(IApplicationBuilder app)
        {
            InitializeIdentityServerDatabase(app);

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        private void InitializeIdentityServerDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                ;
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in Config.ApiScopes)
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
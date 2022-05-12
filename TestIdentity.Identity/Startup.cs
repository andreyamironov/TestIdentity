using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Reflection;
using TestIdentity.Identity.Data;
using TestIdentity.Identity.Repository;
using TestIdentity.Identity.Mapping;
using TestIdentity.Identity.Models;
using TestIdentity.Identity.Infrastructure;
using AMir.Interface.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;

namespace TestIdentity.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(Startup));


            
            //var mapperConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new ClientMapperConfiguration());
            //});
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

            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //services.AddIdentityServer(options =>
            //    {
            //        options.Events.RaiseErrorEvents = true;
            //        options.Events.RaiseInformationEvents = true;
            //        options.Events.RaiseFailureEvents = true;
            //        options.Events.RaiseSuccessEvents = true;

            //        // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
            //        options.EmitStaticAudienceClaim = true;
            //    })
                //.AddConfigurationStore(options =>
                //{
                //    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                //        sql => sql.MigrationsAssembly(migrationsAssemblyIdentityServer));
                //})
                //.AddOperationalStore(options =>
                //{
                //    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                //        sql => sql.MigrationsAssembly(migrationsAssemblyIdentityServer));
                //})
                //.AddAspNetIdentity<ApplicationUser>();
           
          
            services.AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                 options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                 options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
             });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan      = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts     = 3;
                options.Lockout.AllowedForNewUsers          = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
           
            //services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");

            services.AddSession();
            services.AddControllersWithViews();

            //services.AddTransient(typeof(UsersBroker));
            //services.AddTransient(typeof(UserClaimsBroker));
            //services.AddTransient(typeof(IReaderRepository<Client,int>),typeof(Repository.Clients.ClientsReaderRepository));
            //services.AddTransient(typeof(IReaderByOwnerRepository<int,ClientScope, int>), typeof(Repository.ClientScopes.ClientScopesReaderRepository));
            //services.AddTransient(typeof(IReaderByOwnerRepository<int, ClientGrantType, int>), typeof(Repository.ClientGrantTypes.ClientGrantTypesReaderRepository));


            //services.AddTransient(typeof(IWriterRepository<Client>), typeof(Repository.Clients.ClientsWriterRepository));
            //services.AddTransient(typeof(IWriterRepository<ClientScope>), typeof(Repository.ClientScopes.ClientScopesWriterRepository));

            services.AddTransient(typeof(IReaderRepository<ApplicationUser, Guid>), typeof(UsersReaderRepository));
            services.AddTransient(typeof(IReaderRepository<LogEvent, int>), typeof(LogEventsReaderRepository));

            //services.AddTransient(typeof(IdentityServerClientsRepository));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeIdentityServerDatabase(app);

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/Error");
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/html charset=utf-8";

                        await context.Response.WriteAsync("<html lang=\"ru\"><body>\r\n");
                        await context.Response.WriteAsync("<head><meta charset=\"utf-8\"></meta></head>\r\n");

                        await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                        var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();


                        await context.Response.WriteAsync(exceptionHandlerPathFeature?.Error?.Message);
                        await context.Response.WriteAsync("<br></br>");
                        await context.Response.WriteAsync(exceptionHandlerPathFeature?.Error?.InnerException?.Message);
                        await context.Response.WriteAsync("<br></br><div><a href=\"/\">Home</a><br>\r\n</div>");
                        await context.Response.WriteAsync("</body></html>\r\n");
                        await context.Response.WriteAsync(new string(' ', 512)); // IE padding
                    });
                });

            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/html charset=utf-8";

                        await context.Response.WriteAsync("<html lang=\"ru\"><body>\r\n");
                        await context.Response.WriteAsync("<head><meta charset=\"utf-8\"></meta></head>\r\n");

                        await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                        var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();


                        await context.Response.WriteAsync(exceptionHandlerPathFeature?.Error?.Message);
                        await context.Response.WriteAsync("<br></br>");
                        await context.Response.WriteAsync(exceptionHandlerPathFeature?.Error?.InnerException?.Message);
                        await context.Response.WriteAsync("<br></br><div><a href=\"/\">Home</a><br>\r\n</div>");
                        await context.Response.WriteAsync("</body></html>\r\n");
                        await context.Response.WriteAsync(new string(' ', 512)); // IE padding
                    });
                });
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSerilogRequestLogging();
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeIdentityServerDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                //serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                //var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                //context.Database.Migrate();
                //;
                //if (!context.Clients.Any())
                //{
                //    //foreach (var client in Config.Clients)
                //    //{
                //    //    context.Clients.Add(client.ToEntity());
                //    //}
                //    context.SaveChanges();
                //}

                //if (!context.IdentityResources.Any())
                //{
                //    //foreach (var resource in Config.IdentityResources)
                //    //{
                //    //    context.IdentityResources.Add(resource.ToEntity());
                //    //}
                //    context.SaveChanges();
                //}

                //if (!context.ApiScopes.Any())
                //{
                //    //foreach (var resource in Config.ApiScopes)
                //    //{
                //    //    context.ApiScopes.Add(resource.ToEntity());
                //    //}
                //    context.SaveChanges();
                //}
            }
        }
    }
}

﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            //    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            //    .MinimumLevel.Override("System", LogEventLevel.Warning)
            //    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
            //    .Enrich.FromLogContext()
            //    // uncomment to write to Azure diagnostics stream
            //    //.WriteTo.File(
            //    //    @"D:\home\LogFiles\Application\identityserver.txt",
            //    //    fileSizeLimitBytes: 1_000_000,
            //    //    rollOnFileSizeLimit: true,
            //    //    shared: true,
            //    //    flushToDiskInterval: TimeSpan.FromSeconds(1))
            //    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
            //    .CreateLogger();

            //try
            //{
            //    var seed = args.Contains("/seed");
            //    if (seed)
            //    {
            //        args = args.Except(new[] { "/seed" }).ToArray();
            //    }

            //    var host = CreateHostBuilder(args).Build();

            //    if (seed)
            //    {
            //        Log.Information("Seeding database...");
            //        var config = host.Services.GetRequiredService<IConfiguration>();
            //        var connectionString = config.GetConnectionString("DefaultConnection");
            //        //SeedData.EnsureSeedData(connectionString);
            //        Log.Information("Done seeding database.");
            //        return 0;
            //    }

            //    Log.Information("Starting host...");
            //    host.Run();
            //    return 0;
            //}
            //catch (Exception ex)
            //{
            //    Log.Fatal(ex, "Host terminated unexpectedly.");
            //    return 1;
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}

            string connectionStringLogger = string.Empty;
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var config = scope.ServiceProvider.GetService<IConfiguration>();
                connectionStringLogger = config.GetConnectionString("DefaultConnection");
            }

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
            .WriteTo
            .MSSqlServer(
                connectionString: connectionStringLogger,
                sinkOptions: new MSSqlServerSinkOptions { TableName = "Events", SchemaName = "log", AutoCreateSqlTable = true })
            .CreateLogger();


            try
            {
                Log.Information("Getting the motors running...");

                using (var scope = host.Services.CreateScope())
                {
                    await Data.DataInializer.InitializeAcync(scope.ServiceProvider);
                }
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
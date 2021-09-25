using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Microsoft.Extensions.Configuration;

namespace TestIdentity.Identity
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string connectionStringLogger = string.Empty;
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var config = scope.ServiceProvider.GetService<IConfiguration>();
                connectionStringLogger = config.GetConnectionString("DefaultConnection");
            }

            Log.Logger = new LoggerConfiguration()
            .WriteTo
            .MSSqlServer(
                connectionString: connectionStringLogger,
                sinkOptions: new MSSqlServerSinkOptions { TableName = "Events",SchemaName="log" ,AutoCreateSqlTable=true})
            .CreateLogger();


            try
            {
                Log.Information("Getting the motors running...");

                using (var scope = host.Services.CreateScope())
                {
                    await Infrastructure.DataInializer.InitializeAcync(scope.ServiceProvider);
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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .UseSerilog();
    }
}

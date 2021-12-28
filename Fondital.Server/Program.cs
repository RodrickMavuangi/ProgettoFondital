using Fondital.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
using Serilog;

namespace Fondital.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            // Initialize the database
            var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<FonditalDbContext>();
            if (db.Database.EnsureCreated())
            {
                SeedData.Initialize(db);
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
                logging.AddAzureWebAppDiagnostics();
                logging.AddSerilog();
            })
            .ConfigureServices(services =>
            {
                services.Configure<AzureFileLoggerOptions>(options =>
                {
                    options.FileName = "fondital-azure-diagnostics";
                    options.FileSizeLimit = 5 * 1024;
                    options.RetainedFileCountLimit = 5;
                });
            })
            .UseStartup<Startup>();
        //  .ConfigureWebHostDefaults(webBuilder =>
        //  {
        //      webBuilder.UseStartup<Startup>();
        //  });
    }
}
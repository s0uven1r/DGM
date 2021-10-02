using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Resource.Infrastructure.Persistence;
using Resource.Infrastructure.Persistence.Seed;
using Serilog;
using System;
using System.Threading.Tasks;

namespace ResourceAPI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            //Read Configuration from appSettings
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            //Initialize Logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<AppDbContext>();
                    await dbContext.Database.MigrateAsync();

                    await SeedAccountHeadCountTable.SeedAccountHeadCountAsync(dbContext);
                    await SeedAccountTypes.SeedAccountTypesAsync(dbContext);
                    await SeedAccountHeads.SeedAccountHeadsAsync(dbContext);

                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "An error occurred while migrating or seeding the resource api database.");
                    throw;
                }
            }

            try
            {
                Log.Information("Resource API is starting.");
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The Resource API failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
         .UseSerilog()
            //Uses Serilog instead of default .NET Logger
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}

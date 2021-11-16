using AuthServer.Entities;
using AuthServer.Persistence;
using AuthServer.Persistence.Seed;
using Dgm.Common.Models;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Threading.Tasks;

namespace AuthServer
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            //Read Configuration from appSettings
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            //Initialize Logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var clientUrls = services.GetRequiredService<IOptions<ClientBaseUrls>>();
                try
                {
                    //var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                    //await identityContext.Database.MigrateAsync();

                    //await services.GetRequiredService<PersistedGrantDbContext>().Database.MigrateAsync();

                    //var configContext = services.GetRequiredService<ConfigurationDbContext>();
                    //await configContext.Database.MigrateAsync();

                    //var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                    //var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                    //await RoleData.SeedDefaultRolesAsync(roleManager);
                    //await ConfigurationDbContextSeed.SeedDefaultConfiguration(configContext, clientUrls);

                    //await AppIdentityDbContextSeed.SeedDefaultConfiguration(identityContext);
                    //await SeedRolePermission.SeedRolewisePermission(roleManager, identityContext);
                    //await SeedUsers.SeedDefaultUsersAsync(userManager);

                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "An error occurred while migrating or seeding the authserver database.");
                    throw;
                }
            }

            try
            {
                Log.Information("AuthServer is starting.");
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The AuthServer failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configurationBuilder => configurationBuilder.AddEnvironmentVariables())
                .UseSerilog()
                //Uses Serilog instead of default .NET Logger
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        
    }
}

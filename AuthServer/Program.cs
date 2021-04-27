using Auth.Infrastructure.Identity;
using Auth.Infrastructure.Persistence;
using Auth.Infrastructure.Persistence.Seed;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthServer
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    await services.GetRequiredService<AppIdentityDbContext>().Database.MigrateAsync();

                    await services.GetRequiredService<PersistedGrantDbContext>().Database.MigrateAsync();

                    var configcontext = services.GetRequiredService<ConfigurationDbContext>();
                    await configcontext.Database.MigrateAsync();

                    var userManager = scope.ServiceProvider
                  .GetRequiredService<UserManager<AppUser>>();

                    var user = new AppUser
                    {
                        Name = "admin",
                        Email = "admin@dgm.com",
                        UserName = "admin@dgm.com"
                    };
                    userManager.CreateAsync(user, "Password").GetAwaiter().GetResult();

                    await ConfigurationDbContextSeed.SeedDefaultConfiguration(configcontext);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

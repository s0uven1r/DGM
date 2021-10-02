using AuthServer.Configurations;
using Dgm.Common.Models;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Persistence.Seed
{
    public static class ConfigurationDbContextSeed
    {
        public async static Task SeedDefaultConfiguration(ConfigurationDbContext context, IOptions<ClientBaseUrls> clientUrls)
        {
            if (context.Clients.Any())
                context.Clients.RemoveRange(context.Clients);
            if (context.ApiScopes.Any())
                context.ApiScopes.RemoveRange(context.ApiScopes);
            if (context.IdentityResources.Any())
                context.IdentityResources.RemoveRange(context.IdentityResources);
            if (context.ApiResources.Any())
                context.ApiResources.RemoveRange(context.ApiResources);

            context.Clients.AddRange(Configuration.GetClients(clientUrls));
            context.ApiScopes.AddRange(Configuration.GetApiScopes());
            context.IdentityResources.AddRange(Configuration.GetIdentityResources());
            context.ApiResources.AddRange(Configuration.GetApiResources());

            await context.SaveChangesAsync();
        }
    }
}

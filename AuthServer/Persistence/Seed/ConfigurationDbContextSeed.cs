using AuthServer.Configurations;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Persistence.Seed
{
    public static class ConfigurationDbContextSeed
    {
        public async static Task SeedDefaultConfiguration(ConfigurationDbContext context)
        {
            await Task.Run(() =>
            {

                //if (!context.Clients.Any())
                //{
                foreach (var client in Configuration.GetClients())
                {
                    if (!context.Clients.Where(x => x.ClientId == client.ClientId).Any())
                        context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
                //}

                //if (!context.ApiScopes.Any())
                //{
                foreach (var apiscope in Configuration.GetApiScopes())
                {
                    if (!context.ApiScopes.Where(x => x.Name == apiscope.Name).Any())
                        context.ApiScopes.Add(apiscope.ToEntity());
                }
                context.SaveChanges();
                //}

                //if (!context.IdentityResources.Any())
                //{
                foreach (var resource in Configuration.GetIdentityResources())
                {
                    if (!context.IdentityResources.Where(x => x.Name == resource.Name).Any())
                        context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
                //}

                //if (!context.ApiResources.Any())
                //{
                foreach (var resource in Configuration.GetApiResources())
                {
                    if (!context.ApiResources.Where(x => x.Name == resource.Name).Any())
                        context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
                //}
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Infrastructure.Identity;
using Auth.Infrastructure.Persistence;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = true;
            })
                 .AddEntityFrameworkStores<AppIdentityDbContext>()
                 .AddDefaultTokenProviders();
            
            services.AddIdentityServer()
                .AddAspNetIdentity<AppUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 60; // interval in seconds
                })
                .AddDeveloperSigningCredential()
                .AddProfileService<IdentityClaimsProfileService>();

            //.AddInMemoryIdentityResources(Configuration.GetIdentityResources())
            //.AddInMemoryApiScopes(Configuration.GetApiScopes())
            //.AddInMemoryApiResources(Configuration.GetApiResources())
            //.AddInMemoryClients(Configuration.GetClients());

            /* We'll play with this down the road... 
                  services.AddAuthentication()
                  .AddGoogle("Google", options =>
                  {
                      options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                      options.ClientId = "<insert here>";
                      options.ClientSecret = "<insert here>";
                  });*/

            services.AddTransient<IProfileService, IdentityClaimsProfileService>();

            return services;
        }
    }
}

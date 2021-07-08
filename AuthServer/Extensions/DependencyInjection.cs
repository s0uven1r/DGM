using AuthServer.Configurations;
using AuthServer.Entities;
using AuthServer.Persistence;
using AuthServer.Services;
using AuthServer.Services.EmailSender;
using IdentityServer4;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace AuthServer.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionstring = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(connectionstring,
                                                        sql => sql.MigrationsAssembly(migrationsAssembly)
                                                        ));

            services.AddIdentity<AppUser, AppRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = true;
            }).AddEntityFrameworkStores<AppIdentityDbContext>()
              .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromHours(1));

            services.AddIdentityServer()
                .AddAspNetIdentity<AppUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(connectionstring,
                                                     sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(connectionstring,
                                                      sql => sql.MigrationsAssembly(migrationsAssembly));
                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 60; // interval in seconds
                })
                .AddDeveloperSigningCredential()
                .AddProfileService<IdentityClaimsProfileService>();

            services.AddAuthentication().AddLocalApi();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityServerConstants.LocalApi.PolicyName, policy =>
                {
                    policy.AddAuthenticationSchemes(IdentityServerConstants.LocalApi.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    // custom requirements
                });
            });

            services.Configure<EmailSenderConfig>(configuration.GetSection("EmailMailSenderSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}

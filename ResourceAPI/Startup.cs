using Dgm.Common.Error;
using Dgm.Common.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Resource.Application;
using Resource.Application.Command.Customer;
using Resource.Application.Common.Interfaces;
using Resource.Infrastructure;
using Resource.Infrastructure.Service;
using ResourceAPI.Helper.Swagger;
using System;
using System.Collections.Generic;

namespace ResourceAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var IdentityAuthority = Configuration.GetSection("ClientBaseUrls:AuthServer").Value;

            if (_env.IsDevelopment())
            {
                services.Configure<ClientBaseUrls>(Configuration.GetSection("ClientBaseUrls"));
            }
            else
            {
                IdentityAuthority = Environment.GetEnvironmentVariable("URL_AUTHSERVER");
                services.Configure<ClientBaseUrls>(x => ClientBaseUrlsHelper.Configure(x));
            }
            Console.WriteLine(IdentityAuthority);

            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = IdentityAuthority;
                o.Audience = "resourceapi";
                o.RequireHttpsMetadata = false; //research
            });

            //research
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("ApiReader", policy => policy.RequireClaim("scope", "api.read"));
            //    options.AddPolicy("Consumer", policy => policy.RequireClaim(ClaimTypes.Role, "consumer"));
            //});

            services.AddSingleton<IUserAccessor, UserAccessor>();
            services.AddScoped<IAccountHeadCountService, AccountHeadCountService>();
            services.AddHttpContextAccessor();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ResourceAPI", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{IdentityAuthority}/connect/authorize"),
                            TokenUrl = new Uri($"{IdentityAuthority}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"api.read", "api.read"},
                            },
                        }
                    }
                });
                c.OperationFilter<AuthorizationCheckOperationFilter>();
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Resource API v1");
                c.OAuthClientId("demo_api_swagger");
                c.OAuthAppName("Demo API - Swagger");
                c.OAuthUsePkce();
            });

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();


            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

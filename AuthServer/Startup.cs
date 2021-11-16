using AuthServer.Extensions;
using AuthServer.Filters;
using AuthServer.Helpers;
using Dgm.Common.Error;
using Dgm.Common.Models;
using FluentValidation.AspNetCore;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace AuthServer
{
    public class Startup
    {
        public IConfiguration Config { get; }

        private IWebHostEnvironment _env;

        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            Config = config;
            _env = env;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            var identityUrl = Config.GetSection("ClientBaseUrls:AuthServer").Value;

            if (_env.IsDevelopment())
            {
                services.Configure<ClientBaseUrls>(Config.GetSection("ClientBaseUrls"));
            }
            else
            {
                identityUrl = Environment.GetEnvironmentVariable("URL_AUTHSERVER");
                services.Configure<ClientBaseUrls>(x => ClientBaseUrlsHelper.Configure(x));
            }

            services.AddControllersWithViews(options =>
              options.Filters.Add<ValidationFilter>()).AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
              .AddFluentValidation(s =>
              {
                  s.RegisterValidatorsFromAssemblyContaining<Startup>();
              });

            services.AddServices(Config, _env.IsDevelopment());

            services.AddCors(options => options.AddPolicy("AllowAll", p =>
              p.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{identityUrl}/connect/authorize"),
                            TokenUrl = new Uri($"{identityUrl}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"api.read", "api.read"}
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

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth Server v1");
                c.OAuthClientId("demo_api_swagger");
                c.OAuthAppName("Demo API - Swagger");
                c.OAuthUsePkce();
            });

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/debug-config", ctx =>
                {
                    var config = (Config as IConfigurationRoot).GetDebugView();
                    return ctx.Response.WriteAsync(config);
                });
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

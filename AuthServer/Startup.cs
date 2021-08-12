using AuthServer.Extensions;
using AuthServer.Filters;
using AuthServer.Helpers;
using Dgm.Common.Error;
using FluentValidation.AspNetCore;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
        public Startup(IConfiguration config)
        {
            Config = config;
        }
        public IConfiguration Config { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
              options.Filters.Add<ValidationFilter>()).AddNewtonsoftJson(options => 
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
               )
              .AddFluentValidation(s =>
              {
                  s.RegisterValidatorsFromAssemblyContaining<Startup>();
                  //s.RunDefaultMvcValidationAfterFluentValidationExecutes = false; //only support Fluent Validation
              });

            services.AddServices(Config);
           
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
                            AuthorizationUrl = new Uri("https://localhost:44316/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:44316/connect/token"),
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
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth server v1");
                    c.OAuthClientId("demo_api_swagger");
                    c.OAuthAppName("Demo API - Swagger");
                    c.OAuthUsePkce();
                });
            }

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            //app.UseExceptionHandler(builder =>
            //{
            //    builder.Run(async context =>
            //    {
            //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            //        var error = context.Features.Get<IExceptionHandlerFeature>();
            //        if (error != null)
            //        {
            //            context.Response.AddApplicationError(error.Error.Message);
            //            await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
            //        }
            //    });
            //});

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

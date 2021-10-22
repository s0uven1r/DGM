using Dgm.Common.Error;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Resource.Application;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = "https://localhost:44316";
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
                            AuthorizationUrl = new Uri("https://localhost:44316/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:44316/connect/token"),
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
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ResourceAPI v1");
                    c.OAuthClientId("demo_api_swagger");
                    c.OAuthAppName("Demo API - Swagger");
                    c.OAuthUsePkce();
                });
            }

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();

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

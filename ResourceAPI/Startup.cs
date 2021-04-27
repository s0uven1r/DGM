using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(o =>
            //{
            //    o.Authority = "https://localhost:44316";
            //    o.Audience = "resourceapi";
            //    o.RequireHttpsMetadata = false; //research
            //});

            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:44316";
                    options.Audience = "resourceapi";
                });
            //research
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("ApiReader", policy => policy.RequireClaim("scope", "api.read"));
            //    options.AddPolicy("Consumer", policy => policy.RequireClaim(ClaimTypes.Role, "consumer"));
            //});

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
                                {"resourceapi", "Resource API"},
                                {"openid", "openid"},
                                {"profile", "profile"},
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
                    c.OAuthClientId("api_swagger");
                    c.OAuthAppName("Swagger UI");
                    c.OAuthUsePkce();
                });
            }

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

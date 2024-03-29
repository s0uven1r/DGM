using Auth.Infrastructure;
using AuthServer.Filters;
using Dgm.Common.Error;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.AddInfrastructure(Config);

            services.AddCors(options => options.AddPolicy("AllowAll", p =>
              p.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

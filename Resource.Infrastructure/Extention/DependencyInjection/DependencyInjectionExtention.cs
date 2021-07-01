using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Resource.Application.Service.Abstract;
using Resource.Infrastructure.Service.Implementation;

namespace Resource.Infrastructure.Extention.DependencyInjection
{
    public static class DependencyInjectionExtention
    {
        public static IServiceCollection RegisterAllDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            return services;
        }
    }
}

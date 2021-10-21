using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resource.Application.Common.Interfaces;
using Resource.Infrastructure.Persistence;
using Resource.Infrastructure.Service;

namespace Resource.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());

            services.AddTransient<IDateTime, DateTimeService>();
            

            return services;
        }
    }
}

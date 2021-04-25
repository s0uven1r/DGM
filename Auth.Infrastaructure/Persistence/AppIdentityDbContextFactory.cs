using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence
{
    public class AppIdentityDbContextFactory : DesignTimeDbContextFactoryBase<AppIdentityDbContext>
    {
        protected override AppIdentityDbContext CreateNewInstance(DbContextOptions<AppIdentityDbContext> options)
        {
            return new AppIdentityDbContext(options);
        }
    }
}
